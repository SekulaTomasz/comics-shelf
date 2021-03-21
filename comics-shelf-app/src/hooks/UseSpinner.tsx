import { useContext } from 'react';
import { SpinnerContext } from '../contexts/SpinnerContext';

export const useSpinner = (state?: boolean) => useContext(SpinnerContext);