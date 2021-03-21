import { useContext } from 'react';
import { AlertContext, IAlertProps } from '../contexts/AlertContext';

export const useAlert = (alertProps?: IAlertProps) => useContext(AlertContext);