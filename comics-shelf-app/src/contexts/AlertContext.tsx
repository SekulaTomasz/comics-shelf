
import React, { createContext, FunctionComponent, useEffect, useState } from 'react';
import Alert, { Color } from '@material-ui/lab/Alert';

interface IAlertContext {
    toggleAlert: Function;
    alertProps: IAlertProps;
}

export interface IAlertProps {
    message: string;
    severity: string;
    isVisible: boolean;
}

const context = {
    alertProps: {
        isVisible: false,
        severity: "error",
        message: ""
    },
    toggleAlert: () => { }
} as IAlertContext;

export const AlertContext = createContext<IAlertContext>(context);

let timeout: any = null;

export const AlertProvider: FunctionComponent = ({ children }) => {

    const [toastContext, toggleAlertState] = useState<IAlertProps>(context.alertProps);

    const toggleAlert = (alertProps: IAlertProps) => toggleAlertState(alertProps);

    useEffect(() => {
        if(!toastContext.isVisible) {
            clearTimeout(timeout);
            return;
        };
        timeout = setTimeout(timeoutHandler, 5000)
    },[toastContext]);

    const timeoutHandler = () => toggleAlert({...toastContext, isVisible: false,} as IAlertProps)

    

    return <AlertContext.Provider value={{ alertProps:toastContext, toggleAlert: toggleAlert }}>
        {toastContext.isVisible && (<Alert variant="outlined" severity={toastContext.severity as Color} style={{margin: '1rem'}}>
            {toastContext.message}
        </Alert>)}
        {children}
    </AlertContext.Provider>
}