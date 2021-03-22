import { IComics } from "../interfaces/IComics";

const baseUrl = process.env.REACT_APP_API_BASE_URL;

const ComicsService = {
    getComicsByTitle: async(title: string) => {
        return await fetch(`${baseUrl}/comics/byTitle?title=${title}`)
    }
}


export default ComicsService;