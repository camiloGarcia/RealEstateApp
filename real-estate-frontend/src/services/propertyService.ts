import axios from 'axios';
import { Property, CreatePropertyDto, UpdatePropertyDto, PropertyFilter } from '../types/Property';

const API_BASE_URL = 'http://localhost:5010/api';

const propertyService = {
  async getAllProperties(): Promise<Property[]> {
    const response = await axios.get(`${API_BASE_URL}/properties`);
    return response.data;
  },

  async getFilteredProperties(filter: PropertyFilter): Promise<Property[]> {
    const params = new URLSearchParams();
    
    if (filter.name) params.append('name', filter.name);
    if (filter.address) params.append('address', filter.address);
    if (filter.minPrice) params.append('minPrice', filter.minPrice.toString());
    if (filter.maxPrice) params.append('maxPrice', filter.maxPrice.toString());
    if (filter.page) params.append('page', filter.page.toString());
    if (filter.pageSize) params.append('pageSize', filter.pageSize.toString());

    const response = await axios.get(`${API_BASE_URL}/properties?${params.toString()}`);
    return response.data;
  },

  async getPropertyById(id: string): Promise<Property> {
    const response = await axios.get(`${API_BASE_URL}/properties/${id}`);
    return response.data;
  },

  async createProperty(property: CreatePropertyDto): Promise<Property> {
    const response = await axios.post(`${API_BASE_URL}/properties`, property);
    return response.data;
  },

  async updateProperty(id: string, property: UpdatePropertyDto): Promise<Property> {
    const response = await axios.put(`${API_BASE_URL}/properties/${id}`, property);
    return response.data;
  },

  async deleteProperty(id: string): Promise<void> {
    await axios.delete(`${API_BASE_URL}/properties/${id}`);
  }
};

export default propertyService;
