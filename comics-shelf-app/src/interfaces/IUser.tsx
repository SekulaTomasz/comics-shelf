import { IEntity } from "./IEntity";

export interface IUser extends IEntity {
    login: string;
    coins: Number;
}