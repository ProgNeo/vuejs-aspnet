import http from '../http-common'

class GenreService {
    getAll() {
        return http.get("/Genres")
    }

    create(data) {
        return http.post('/Genres', data)
    }

    delete(id) {
        return http.delete(`/Genres/${id}`)
    }
}

export default new GenreService()