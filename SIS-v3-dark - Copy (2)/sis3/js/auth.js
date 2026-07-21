// ============================================
// auth.js — Guard, Logout, and two distinct nav
// styles: a console sidebar for Admin/Teacher,
// and a page-like topbar for Student.
// ============================================

function requireAuth() {
  const user = getCurrentUser();
  if (!user || !getToken()) {
    window.location.href = "login.html";
    return null;
  }
  const currentPage = window.location.pathname.split("/").pop();
  if (user.mustChangePassword && currentPage !== "force-change-password.html") {
    window.location.href = "force-change-password.html";
    return null;
  }
  return user;
}

function requireRole(allowedRoles) {
  const user = requireAuth();
  if (!user) return null;
  if (!allowedRoles.includes(user.role)) {
    alert("Bu səhifəyə giriş icazəniz yoxdur.");
    window.location.href = "dashboard.html";
    return null;
  }
  return user;
}

function logout() {
  clearCurrentUser();
  window.location.href = "login.html";
}

function getNavLinks(role) {
  const common = [{ href: "dashboard.html", label: "Dashboard", ic: "\u2302" }];
  if (role === "Student") {
    return [
      ...common,
      { href: "courses.html", label: "Kurslar", ic: "\u25A6" },
      { href: "enrollments.html", label: "Qeydiyyatım", ic: "\u2713" },
      { href: "grades.html", label: "Qiymətlərim", ic: "\u2605" },
      { href: "attendance.html", label: "Davamiyyətim", ic: "\u25F7" },
      { href: "announcements.html", label: "Elanlar", ic: "\u26A1" },
      { href: "chat.html", label: "Söhbət", ic: "\u2709" },
    ];
  }
  if (role === "Teacher") {
    return [
      ...common,
      { href: "courses.html", label: "Kurslarım", ic: "\u25A6" },
      { href: "grades.html", label: "Qiymət Gir", ic: "\u2605" },
      { href: "attendance.html", label: "Davamiyyət", ic: "\u25F7" },
      { href: "announcements.html", label: "Elanlar", ic: "\u26A1" },
      { href: "chat.html", label: "Söhbət", ic: "\u2709" },
    ];
  }
  return [
    ...common,
    { href: "students.html", label: "Tələbələr", ic: "\u25A4" },
    { href: "courses.html", label: "Kurslar", ic: "\u25A6" },
    { href: "announcements.html", label: "Elanlar", ic: "\u26A1" },
    { href: "admin.html", label: "Admin Panel", ic: "\u2699" },
  ];
}

// Admin/Teacher — dense sidebar console
function renderSidebar(activePage) {
  const user = getCurrentUser();
  if (!user) return "";
  const links = getNavLinks(user.role)
    .map(l => `<a href="${l.href}" class="${activePage === l.href ? 'active' : ''}"><span class="ic">${l.ic}</span>${l.label}</a>`)
    .join("");
  return `
    <aside class="sidebar">
      <div class="brand"><span class="dot"></span>SIS
        <button class="theme-toggle" style="margin-left:auto" onclick="toggleTheme()" title="Tema dəyiş" aria-label="Tema dəyiş">
          <span class="icon-sun">☀</span><span class="icon-moon">☾</span>
        </button>
      </div>
      <nav>${links}</nav>
      <div class="user-box">
        <div class="email">${user.email}</div>
        <span class="role-tag">${user.role}</span>
        <button class="logout" onclick="logout()">Çıxış et</button>
      </div>
    </aside>`;
}

// Student — a light, page-like topbar (not a console)
function renderStudentTopbar(activePage) {
  const user = getCurrentUser();
  if (!user) return "";
  const links = getNavLinks("Student")
    .map(l => `<a href="${l.href}" class="${activePage === l.href ? 'active' : ''}">${l.label}</a>`)
    .join("");
  return `
    <header class="student-topbar">
      <span class="brand">Scholar</span>
      <nav>${links}</nav>
      <span class="user-chip">${user.email}</span>
      <button class="theme-toggle" onclick="toggleTheme()" title="Tema dəyiş" aria-label="Tema dəyiş">
        <span class="icon-sun">☀</span><span class="icon-moon">☾</span>
      </button>
      <button class="logout" onclick="logout()">Çıxış</button>
    </header>`;
}

function mountNav(activePage) {
  const user = getCurrentUser();
  if (!user) return;
  const mount = document.getElementById("nav-mount");
  if (!mount) return;

  if (user.role === "Student") document.body.classList.add("student-mode");

  // The dashboard gets the full "page, not a panel" student treatment.
  // Every other shared page keeps the sidebar console (recolored for Student
  // via body.student-mode) so the whole app doesn't need two full layouts.
  if (user.role === "Student" && activePage === "dashboard.html") {
    mount.outerHTML = renderStudentTopbar(activePage);
  } else {
    mount.outerHTML = `<div id="sidebar-mount">${renderSidebar(activePage)}</div>`;
  }
}
