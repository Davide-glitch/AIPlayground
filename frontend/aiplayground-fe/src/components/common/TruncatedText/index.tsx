import React, { useState } from 'react';
import { Box, Typography } from '@mui/material';
import './TruncatedText.css';

interface TruncatedTextProps {
  text: string;
  maxLength?: number;
}

export const TruncatedText: React.FC<TruncatedTextProps> = ({ 
  text, 
  maxLength = 100
}) => {
  const [isExpanded, setIsExpanded] = useState(false);

  if (!text) {
    return <Typography variant="body2">-</Typography>;
  }

  const shouldTruncate = text.length > maxLength;

  if (!shouldTruncate) {
    return (
      <Typography variant="body2" component="div">
        {text}
      </Typography>
    );
  }

  const truncatedText = text.substring(0, maxLength);

  return (
    <Box 
      className="truncated-text-container"
      sx={{ cursor: 'pointer', textAlign: 'center' }}
      onClick={(e) => {
        e.stopPropagation();
        setIsExpanded(!isExpanded);
      }}
    >
      <Typography 
        variant="body2" 
        component="div"
        sx={{ 
          wordBreak: 'break-word',
          whiteSpace: isExpanded ? 'normal' : 'nowrap',
          overflow: 'hidden',
          textOverflow: isExpanded ? 'clip' : 'ellipsis'
        }}
      >
        {isExpanded ? text : `${truncatedText}...`}
      </Typography>
    </Box>
  );
};
