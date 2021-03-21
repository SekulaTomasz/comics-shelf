const baseUrl = process.env.REACT_APP_API_BASE_URL;

const UserService = {
    loginUser: async(login: string, password: string) => {
        return await fetch(`${baseUrl}/users/login`, {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                login:login,
                password: password
            })
        })
    }
}


export default UserService;