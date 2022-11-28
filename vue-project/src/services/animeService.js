import http from '../http-common'

class AnimeService {
    getAll() {
        return http.get("/Anime")
    }

    get(id) {
        return http.get(`/Anime/${id}`)
    }

    create(data) {
        return http.post('/Anime', data)
    }

    update(id, data) {
        return http.put(`/Anime/${id}`, data)
    }

    delete(id) {
        return http.delete(`/Anime/${id}`)
    }

    findByGenre(genreId) {
        return http.get('/Anime', {
            params: {
                genreId: genreId
            }
        })
    }
}

export default new AnimeService()