import { IComics } from "../interfaces/IComics";

const baseUrl = process.env.REACT_APP_API_BASE_URL;

const PurchaseComicsService = {
    purchaseComics: async(comics: IComics, userId: string, asExlusive: boolean) => {

        const data = {
            comics,
            userId,
            asExlusive
        }
        return await fetch(`${baseUrl}/purchaseComics/purchase`, {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(data)
        })
    },
    returnComicsAsync: async(userId: string, comicsId: string) => {
        const data = {
            userId,
            comicsId
        }
        return await fetch(`${baseUrl}/purchaseComics/return`, {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(data)
        })
    },
    downloadFile: async() => {
        return await fetch(`${baseUrl}/purchaseComics/download`) 
    },
    canUserDownload: async(userId: string, comicsId: string) => {
        return await fetch(`${baseUrl}/purchaseComics/canUserDownload?userId=${userId}&comicsId=${comicsId}`);
    }
}


export default PurchaseComicsService;