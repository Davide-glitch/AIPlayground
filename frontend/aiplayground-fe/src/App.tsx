import { Outlet } from "react-router-dom";
import "./App.css";
import { createTheme, ThemeProvider } from "@mui/material";
import { Menu } from "./components/Menu";
import { useEffect } from "react";
import { ErrorBoundary } from "./components/common/ErrorBoundary";

function App() {
  // Suppress ResizeObserver errors
  useEffect(() => {
    const errorHandler = (e: ErrorEvent) => {
      if (e.message?.includes('ResizeObserver loop completed')) {
        e.preventDefault();
        e.stopPropagation();
        return false;
      }
    };

    window.addEventListener('error', errorHandler);
    
    return () => {
      window.removeEventListener('error', errorHandler);
    };
  }, []);

  const theme = createTheme({
    palette: {
      mode: 'light',
      primary: {
        main: "#6366f1", // Modern indigo
        light: "#818cf8",
        dark: "#4f46e5",
        contrastText: "#ffffff",
      },
      secondary: {
        main: "#8b5cf6", // Modern purple
        light: "#a78bfa",
        dark: "#7c3aed",
        contrastText: "#ffffff",
      },
      background: {
        default: "#f8fafc",
        paper: "rgba(255, 255, 255, 0.8)",
      },
      text: {
        primary: "#1e293b",
        secondary: "#64748b",
      },
    },
    typography: {
      fontFamily: '"Inter", "Roboto", "Helvetica", "Arial", sans-serif',
      h1: {
        fontWeight: 800,
        fontSize: '2.5rem',
        background: 'linear-gradient(135deg, #6366f1 0%, #8b5cf6 100%)',
        WebkitBackgroundClip: 'text',
        WebkitTextFillColor: 'transparent',
      },
      h2: {
        fontWeight: 700,
        fontSize: '2rem',
        color: '#1e293b',
      },
      h3: {
        fontWeight: 600,
        fontSize: '1.5rem',
        color: '#1e293b',
      },
      body1: {
        fontSize: '1rem',
        lineHeight: 1.6,
      },
    },
    shape: {
      borderRadius: 16,
    },
    components: {
      MuiCard: {
        styleOverrides: {
          root: {
            background: 'rgba(255, 255, 255, 0.8)',
            backdropFilter: 'blur(20px)',
            border: '1px solid rgba(255, 255, 255, 0.2)',
            boxShadow: '0 8px 32px rgba(0, 0, 0, 0.1)',
            transition: 'all 0.3s ease-in-out',
            '&:hover': {
              transform: 'translateY(-4px)',
              boxShadow: '0 12px 40px rgba(0, 0, 0, 0.15)',
            },
          },
        },
      },
      MuiButton: {
        styleOverrides: {
          root: {
            textTransform: 'none',
            fontWeight: 600,
            borderRadius: 12,
            padding: '10px 24px',
            transition: 'all 0.2s ease-in-out',
            '&:hover': {
              transform: 'translateY(-2px)',
              boxShadow: '0 8px 25px rgba(0, 0, 0, 0.15)',
            },
          },
          contained: {
            background: 'linear-gradient(135deg, #6366f1 0%, #8b5cf6 100%)',
            '&:hover': {
              background: 'linear-gradient(135deg, #4f46e5 0%, #7c3aed 100%)',
            },
          },
        },
      },
      MuiChip: {
        styleOverrides: {
          root: {
            fontWeight: 600,
            borderRadius: 8,
          },
        },
      },
      MuiPaper: {
        styleOverrides: {
          root: {
            background: 'rgba(255, 255, 255, 0.9)',
            backdropFilter: 'blur(20px)',
            border: '1px solid rgba(255, 255, 255, 0.2)',
          },
        },
      },
    },
  });

  return (
    <ErrorBoundary>
      <ThemeProvider theme={theme}>
        <Menu />
        <Outlet />
      </ThemeProvider>
    </ErrorBoundary>
  );
}

export default App;
