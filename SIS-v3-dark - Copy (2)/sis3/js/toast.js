// Lightweight toast/snackbar notifications — bottom-right, auto-dismiss.
// Usage: showToast("Mesaj göndərildi", "success")
// Types: "success" | "error" | "warning" | "info" (default)

function ensureToastContainer() {
  let container = document.getElementById("toast-container");
  if (!container) {
    container = document.createElement("div");
    container.id = "toast-container";
    container.className = "toast-container";
    document.body.appendChild(container);
  }
  return container;
}

function showToast(message, type = "info", duration = 3500) {
  const container = ensureToastContainer();
  const toast = document.createElement("div");
  toast.className = `toast toast-${type}`;
  toast.textContent = message;
  container.appendChild(toast);

  requestAnimationFrame(() => toast.classList.add("toast-visible"));

  const remove = () => {
    toast.classList.remove("toast-visible");
    toast.addEventListener("transitionend", () => toast.remove(), { once: true });
  };
  toast.addEventListener("click", remove);
  setTimeout(remove, duration);
}
