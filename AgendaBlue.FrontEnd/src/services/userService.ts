import api from './api';
import { User } from 'src/models/user';

export default class UserService {
    // Obtém todos os usuários
    static async getAll(): Promise<User[]> {
        const response = await api.get<User[]>('/users');
        return response.data;
    }

    // Obtém um único usuário por ID
    static async getById(id: number): Promise<User> {
        const response = await api.get<User>(`/users/${id}`);
        return response.data;
    }

    // Cria um novo usuário
    static async create(user: User): Promise<User> {
        const response = await api.post<User>('/users', user);
        return response.data;
    }

    // Atualiza um usuário existente
    static async update(id: number, user: User): Promise<User> {
        const response = await api.put<User>(`/users/${id}`, user);
        return response.data;
    }

    // Remove um usuário
    static async delete(id: number): Promise<void> {
        await api.delete(`/users/${id}`);
    }
}
