
import React, { createContext, FunctionComponent, useState } from 'react';
import Alert, { Color } from '@material-ui/lab/Alert';

interface IAlertContext {
    severity: string;
    isVisible: boolean;
    toggleAlert: Function;
    message: string;
}

const context = {
    isVisible: false,
    severity: "error",
    message: "",
    toggleAlert: () => { }
} as IAlertContext;

export const AlertContext = createContext<IAlertContext>(context);


export const AlertProvider: FunctionComponent = ({ children }) => {

    const [toastContext, toggleToastr] = useState<IAlertContext>(context);

    return <AlertContext.Provider value={{ ...toastContext, toggleAlert: toggleToastr }}>
        {toastContext.isVisible && (<Alert variant="outlined" severity={toastContext.severity as Color}>
            {toastContext.message}
        </Alert>)}
        {children}
    </AlertContext.Provider>
}