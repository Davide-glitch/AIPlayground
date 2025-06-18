import React, { useState } from 'react';
import {
  Button,
  Card,
  CardContent,
  Typography,
  Alert,
  CircularProgress,
  Stack,
  Chip
} from '@mui/material';
import { AIPlaygroundApiClient } from '../../../api/Base/BaseApiClient';

export const DiagnosticTools: React.FC = () => {
  const [isChecking, setIsChecking] = useState(false);
  const [connectionStatus, setConnectionStatus] = useState<'unknown' | 'success' | 'error'>('unknown');
  const [errorDetails, setErrorDetails] = useState<string>('');

  const checkBackendHealth = async () => {
    setIsChecking(true);
    setConnectionStatus('unknown');
    setErrorDetails('');

    try {
      // Try a simple GET request to test connectivity
      const response = await AIPlaygroundApiClient.get('/health', {
        timeout: 5000 // 5 second timeout
      });
      
      console.log('Health check response:', response);
      setConnectionStatus('success');
    } catch (error: any) {
      console.error('Health check failed:', error);
      setConnectionStatus('error');
      
      if (error.code === 'ECONNREFUSED' || error.code === 'ERR_NETWORK') {
        setErrorDetails('Connection refused - Backend server is not running or not accessible');
      } else if (error.code === 'ENOTFOUND') {
        setErrorDetails('Server not found - Check if the backend URL is correct');
      } else if (error.response) {
        setErrorDetails(`Server responded with error: ${error.response.status} - ${error.response.statusText}`);
      } else if (error.request) {
        setErrorDetails('No response from server - Check if backend is running');
      } else {
        setErrorDetails(`Request setup error: ${error.message}`);
      }
    } finally {
      setIsChecking(false);
    }
  };

  const getStatusColor = () => {
    switch (connectionStatus) {
      case 'success': return 'success';
      case 'error': return 'error';
      default: return 'default';
    }
  };

  const getStatusText = () => {
    switch (connectionStatus) {
      case 'success': return 'Connected';
      case 'error': return 'Connection Failed';
      default: return 'Unknown';
    }
  };

  return (
    <Card sx={{ maxWidth: 600, margin: 2 }}>
      <CardContent>
        <Typography variant="h6" gutterBottom>
          Backend Connectivity Diagnostic
        </Typography>
        
        <Stack spacing={2}>
          <Typography variant="body2" color="text.secondary">
            Backend URL: {AIPlaygroundApiClient.defaults.baseURL}
          </Typography>
          
          <Stack direction="row" spacing={2} alignItems="center">
            <Button 
              variant="contained" 
              onClick={checkBackendHealth}
              disabled={isChecking}
              startIcon={isChecking ? <CircularProgress size={20} /> : null}
            >
              {isChecking ? 'Checking...' : 'Test Connection'}
            </Button>
            
            {connectionStatus !== 'unknown' && (
              <Chip 
                label={getStatusText()} 
                color={getStatusColor()} 
                variant="filled"
              />
            )}
          </Stack>

          {connectionStatus === 'error' && errorDetails && (
            <Alert severity="error">
              <Typography variant="body2">
                <strong>Error Details:</strong> {errorDetails}
              </Typography>
              <Typography variant="body2" sx={{ mt: 1 }}>
                <strong>Troubleshooting:</strong>
                <ul>
                  <li>Make sure your backend API server is running</li>
                  <li>Check if it's accessible at: {AIPlaygroundApiClient.defaults.baseURL}</li>
                  <li>Verify CORS settings if backend is running</li>
                  <li>Check browser developer console for more details</li>
                </ul>
              </Typography>
            </Alert>
          )}

          {connectionStatus === 'success' && (
            <Alert severity="success">
              Backend connection is working! The API server is responding correctly.
            </Alert>
          )}
        </Stack>
      </CardContent>
    </Card>
  );
};
