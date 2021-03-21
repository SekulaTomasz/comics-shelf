  
import React, { createContext, FunctionComponent, useEffect, useState } from 'react';
import { IUser } from '../interfaces/IUser';
import { useQuery } from "react-query";


interface IUserContext {
    user: IUser | null;
    setContext: Function;
}

export const UserContext = createContext<IUserContext>({
    user: null,
    setContext: () => {}
} as IUserContext);


export const UserProvider: FunctionComponent = ({ children }) => {

    const [context, setContext] = useState<IUserContext>({} as IUserContext);

    return <UserContext.Provider value={{...context, setContext:setContext}}>
        {children}
    </UserContext.Provider>
}