import { IComics } from "./IComics";
import { IEntity } from "./IEntity";
import { IUser } from "./IUser";

export interface IPurchasedComicsUsers extends IEntity {
    
    purchasedAsExclusive: boolean;
    purchaseDate: Date;
    cost: Number;
    user: IUser;
    comics: IComics;
}
