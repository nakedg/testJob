import Axios from 'axios';
import qs from 'qs';

const baseUrl = '/api/';

class HttpService {
    constructor() {
        this.http = Axios.create({
            baseURL: baseUrl, paramsSerializer: function (params) {
                return qs.stringify(params, { encode: false, allowDots: true });
            } /*obj => {
                return '111';
                //return qs.stringify(obj, { arrayFormat: 'brackets', encode: false, indices: false  })
            }*/
        });
    }
}

export default new HttpService();