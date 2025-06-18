import { createRoot } from "react-dom/client";
import { AppRoutes } from "./configs/AppRoutes";
import { BrowserRouter } from "react-router-dom";

// Suppress ResizeObserver loop completed errors
const resizeObserverErrorHandler = (e: ErrorEvent) => {
  if (e.message === 'ResizeObserver loop completed with undelivered notifications.') {
    const resizeObserverErrDiv = document.getElementById('webpack-dev-server-client-overlay-div');
    const resizeObserverErr = document.getElementById('webpack-dev-server-client-overlay');
    if (resizeObserverErr) {
      resizeObserverErr.setAttribute('style', 'display: none');
    }
    if (resizeObserverErrDiv) {
      resizeObserverErrDiv.setAttribute('style', 'display: none');
    }
  }
};
window.addEventListener('error', resizeObserverErrorHandler);

// Also suppress unhandled promise rejections for ResizeObserver
window.addEventListener('unhandledrejection', (e) => {
  if (e.reason?.message?.includes?.('ResizeObserver loop completed')) {
    e.preventDefault();
  }
});

const container = document.getElementById("root");

if (container) {
  const root = createRoot(container);

  root.render(
    <BrowserRouter>
      <AppRoutes />
    </BrowserRouter>
  );
}
