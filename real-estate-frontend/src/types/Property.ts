export interface Property {
  id: string;
  idOwner: string;
  name: string;
  address: string;
  price: number;
  imageUrl: string;
}

export interface CreatePropertyDto {
  idOwner: string;
  name: string;
  address: string;
  price: number;
  imageUrl: string;
}

export interface UpdatePropertyDto {
  name: string;
  address: string;
  price: number;
  imageUrl: string;
}

export interface PropertyFilter {
  name?: string;
  address?: string;
  minPrice?: number;
  maxPrice?: number;
  page?: number;
  pageSize?: number;
}
