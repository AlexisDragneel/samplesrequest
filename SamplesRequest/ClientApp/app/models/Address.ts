import { Client } from "./Client";

export class Address {
    id: number;
    street_number: string;
    zip: string;
    facility: string;
    phone: string;
    country: string;
    state: string;
    city: string;
    client_id: number;
    client: Client = new Client();
}