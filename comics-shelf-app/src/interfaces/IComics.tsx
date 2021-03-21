import { IEntity } from "./IEntity";

export interface IComics extends IEntity {
        publisher: string;
        description: string;
        title: string;
        price: string;
        creators: string;
        release_date: Date;
        diamondId: string;
}