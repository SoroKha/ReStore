export interface BasketItem {
    productID: number;
    name: string;
    price: number;
    pictureURL: string;
    brand: string;
    type: string;
    quantity: number;
}

export interface Basket {
    id: number;
    buyerID: string;
    items: BasketItem[];
}