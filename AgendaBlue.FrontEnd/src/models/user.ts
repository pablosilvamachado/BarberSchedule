export class User {
    id: number | null;
    nome: string;
    email: string;
    telefone: string;
    tipo: 'cliente' | 'profissional';

    constructor({
        id = null,
        nome = '',
        email = '',
        telefone = '',
        tipo = 'cliente',
    }: {
        id?: number | null;
        nome?: string;
        email?: string;
        telefone?: string;
        tipo?: 'cliente' | 'profissional';
    }) {
        this.id = id;
        this.nome = nome;
        this.email = email;
        this.telefone = telefone;
        this.tipo = tipo;
    }

    // Método para validar se o usuário é válido
    isValid(): boolean {
        return !!this.nome && !!this.email && !!this.telefone;
    }
}
