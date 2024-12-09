export class Notificacao {
    id: number | null;
    tipo: 'email' | 'sms';
    destinatario: string; // Email ou telefone
    mensagem: string;
    enviada: boolean;
    dataEnvio: string | null;

    constructor({
        id = null,
        tipo = 'email',
        destinatario = '',
        mensagem = '',
        enviada = false,
        dataEnvio = null,
    }: {
        id?: number | null;
        tipo?: 'email' | 'sms';
        destinatario?: string;
        mensagem?: string;
        enviada?: boolean;
        dataEnvio?: string | null;
    }) {
        this.id = id;
        this.tipo = tipo;
        this.destinatario = destinatario;
        this.mensagem = mensagem;
        this.enviada = enviada;
        this.dataEnvio = dataEnvio;
    }
}
