import React, { useState } from 'react';
import { PropertyFilter } from '../types/Property';

interface PropertyFiltersProps {
  onFilterChange: (filter: PropertyFilter) => void;
  onClearFilters: () => void;
}

const PropertyFilters: React.FC<PropertyFiltersProps> = ({ onFilterChange, onClearFilters }) => {
  const [filters, setFilters] = useState<PropertyFilter>({
    name: '',
    address: '',
    minPrice: undefined,
    maxPrice: undefined,
    page: 1,
    pageSize: 10
  });

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    const newFilters = { ...filters };
    
    if (name === 'minPrice' || name === 'maxPrice') {
      (newFilters as any)[name] = value ? parseFloat(value) : undefined;
    } else {
      (newFilters as any)[name] = value;
    }
    
    setFilters(newFilters);
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    onFilterChange(filters);
  };

  const handleClear = () => {
    const clearedFilters = {
      name: '',
      address: '',
      minPrice: undefined,
      maxPrice: undefined,
      page: 1,
      pageSize: 10
    };
    setFilters(clearedFilters);
    onClearFilters();
  };

  return (
    <div className="filters">
      <form onSubmit={handleSubmit}>
        <div className="filters-grid">
          <div className="form-group">
            <label htmlFor="name">Property Name</label>
            <input
              type="text"
              id="name"
              name="name"
              value={filters.name || ''}
              onChange={handleInputChange}
              placeholder="Search by name..."
            />
          </div>
          
          <div className="form-group">
            <label htmlFor="address">Address</label>
            <input
              type="text"
              id="address"
              name="address"
              value={filters.address || ''}
              onChange={handleInputChange}
              placeholder="Search by address..."
            />
          </div>
          
          <div className="form-group">
            <label htmlFor="minPrice">Min Price</label>
            <input
              type="number"
              id="minPrice"
              name="minPrice"
              value={filters.minPrice || ''}
              onChange={handleInputChange}
              placeholder="Min price..."
              min="0"
            />
          </div>
          
          <div className="form-group">
            <label htmlFor="maxPrice">Max Price</label>
            <input
              type="number"
              id="maxPrice"
              name="maxPrice"
              value={filters.maxPrice || ''}
              onChange={handleInputChange}
              placeholder="Max price..."
              min="0"
            />
          </div>
          
          <div className="form-group">
            <button type="submit" className="btn btn-primary">
              Search
            </button>
          </div>
          
          <div className="form-group">
            <button type="button" className="btn btn-secondary" onClick={handleClear}>
              Clear Filters
            </button>
          </div>
        </div>
      </form>
    </div>
  );
};

export default PropertyFilters;
