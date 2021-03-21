
import React, { createContext, FunctionComponent, useEffect, useState } from 'react';
import Spinner from '../components/Spinner/Spinner';

interface ISpinnerContext {
    toggleSpinner: Function;
}


export const SpinnerContext = createContext<ISpinnerContext>({
    toggleSpinner: () => { },
});


export const SpinnerProvider: FunctionComponent = ({ children }) => {

    const [isVisible,setSpinnerVisible] = useState<boolean>(false);

    const toggleSpinner = (state?: boolean) => state === undefined ? setSpinnerVisible((prevState: boolean) => !prevState) : setSpinnerVisible(state)

    return <SpinnerContext.Provider value={{toggleSpinner}}>
        {isVisible && <Spinner />}
        {children}
    </SpinnerContext.Provider>
}