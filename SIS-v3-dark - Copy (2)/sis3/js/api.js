// ============================================
// api.js — bütün API çağırışları BURADAN keçir
// ============================================

// ---- Tema (tünd/işıqlı) — hər səhifədə tətbiq olunur, seçim localStorage-da saxlanılır ----
const THEME_KEY = "sis_theme";

function getTheme() {
  return localStorage.getItem(THEME_KEY) || "dark";
}

function setTheme(theme) {
  localStorage.setItem(THEME_KEY, theme);
  document.documentElement.setAttribute("data-theme", theme);
}

function toggleTheme() {
  setTheme(getTheme() === "dark" ? "light" : "dark");
}

// Boyanmadan əvvəl tətbiq olunur ki, səhifə açılanda köhnə/yanlış tema bir anlıq görünməsin
document.documentElement.setAttribute("data-theme", getTheme());

// Backend-in ünvanı — https profili (sertifikatı bir dəfə brauzerdə qəbul etməlisən)
const BASE_URL = "https://localhost:7088/api";

// localStorage-dan token oxuyur
function getToken() {
  return localStorage.getItem("sis_token");
}

// localStorage-dan login olan istifadəçinin məlumatını oxuyur
function getCurrentUser() {
  const raw = localStorage.getItem("sis_user");
  return raw ? JSON.parse(raw) : null;
}

// Login/Register uğurlu olanda bu məlumatı saxlayırıq
function setCurrentUser(authResponse) {
  localStorage.setItem("sis_token", authResponse.token);
  localStorage.setItem("sis_user", JSON.stringify({
    email: authResponse.email,
    role: authResponse.role,
    userId: authResponse.userId,
    studentId: authResponse.studentId,
    teacherId: authResponse.teacherId,
    mustChangePassword: authResponse.mustChangePassword
  }));
}

function clearCurrentUser() {
  localStorage.removeItem("sis_token");
  localStorage.removeItem("sis_user");
}

// Bütün endpoint çağırışları bu funksiyadan keçir
// method: GET/POST/PUT/DELETE, body: JS obyekti (JSON-a çevrilir)
async function apiRequest(endpoint, method = "GET", body = null) {
  const headers = { "Content-Type": "application/json" };
  const token = getToken();
  if (token) headers["Authorization"] = `Bearer ${token}`;

  const response = await fetch(`${BASE_URL}${endpoint}`, {
    method,
    headers,
    body: body ? JSON.stringify(body) : null
  });

  // 204 No Content kimi hallarda body olmaya bilər
  let data = null;
  try { data = await response.json(); } catch (e) { /* body yoxdur */ }

  // Backend ResponseModel<T> formatında qaytarır:
  // { success, statusCode, data, errors }
  if (!response.ok) {
    const message = (data && data.errors && data.errors.length)
      ? data.errors.join(", ")
      : `Xəta baş verdi (${response.status})`;
    throw new Error(message);
  }

  return data; // { success, statusCode, data, errors }
}

// PDF kimi fayl endpoint-ləri üçün ayrı funksiya (JSON qaytarmır, blob qaytarır)
async function apiDownload(endpoint, filename) {
  const token = getToken();
  const response = await fetch(`${BASE_URL}${endpoint}`, {
    headers: { "Authorization": `Bearer ${token}` }
  });

  if (!response.ok) throw new Error("Fayl endirilə bilmədi");

  const blob = await response.blob();
  const url = window.URL.createObjectURL(blob);
  const a = document.createElement("a");
  a.href = url;
  a.download = filename;
  document.body.appendChild(a);
  a.click();
  a.remove();
  window.URL.revokeObjectURL(url);
}
