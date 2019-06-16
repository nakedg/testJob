import axios from './httpService';

class UserService {
    getUsers(page, pageSize, sort, filter) {
        var request = { page, pageSize, sort, filter };
        return axios.http.get('user', { params: request })
            .then(resp => {
                return resp.data;
            });
    }

    getUser(id) {
        return axios.http.get('user/' + id)
            .then(resp => {
                return resp.data;
            });
    }

    createUser(user) {
        return axios.http.post('user', user)
            .then(resp => {
                return resp.data;
            });
    }

    editUser(user) {
        return axios.http.put('user', user)
            .then(resp => {
                return resp.data;
            });
    }

    deleteUser(id) {
        return axios.http.delete('user/' + id)
            .then(resp => {
                return resp.data;
            });
    }
}

export default new UserService();