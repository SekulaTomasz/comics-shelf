import { IEntity } from "./IEntity";

export interface IComics extends IEntity {
        publisher: string;
        description: string;
        title: string;
        price: string;
        creators: string;
        releaseDate: Date;
        diamondId: string;
}