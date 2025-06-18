import React, { Component, ErrorInfo, ReactNode } from 'react';

interface Props {
  children: ReactNode;
}

interface State {
  hasError: boolean;
  error?: Error;
}

export class ErrorBoundary extends Component<Props, State> {
  constructor(props: Props) {
    super(props);
    this.state = { hasError: false };
  }

  static getDerivedStateFromError(error: Error): State {
    // Update state so the next render will show the fallback UI
    if (error.message?.includes('ResizeObserver loop completed')) {
      // Don't show error UI for ResizeObserver errors
      return { hasError: false };
    }
    return { hasError: true, error };
  }

  componentDidCatch(error: Error, errorInfo: ErrorInfo) {
    // Suppress ResizeObserver errors
    if (error.message?.includes('ResizeObserver loop completed')) {
      return;
    }
    
    console.error('Error caught by boundary:', error, errorInfo);
  }

  render() {
    if (this.state.hasError) {
      // You can render any custom fallback UI
      return (
        <div style={{ 
          padding: '2rem', 
          textAlign: 'center', 
          color: '#ef4444',
          background: 'rgba(255, 255, 255, 0.9)',
          borderRadius: '12px',
          margin: '2rem'
        }}>
          <h2>Something went wrong.</h2>
          <p>An error occurred in the application.</p>
        </div>
      );
    }

    return this.props.children;
  }
}
