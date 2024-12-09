export interface User {
    id: number | null;
    nome: string;
    email: string;
    telefone: string;
    tipo: 'cliente' | 'profissional';
}