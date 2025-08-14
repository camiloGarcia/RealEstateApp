import React from 'react';
import { Property } from '../types/Property';
import PropertyCard from './PropertyCard';

interface PropertyListProps {
  properties: Property[];
  onViewDetails: (property: Property) => void;
  loading: boolean;
  error: string | null;
}

const PropertyList: React.FC<PropertyListProps> = ({ 
  properties, 
  onViewDetails, 
  loading, 
  error 
}) => {
  if (loading) {
    return <div className="loading">Loading properties...</div>;
  }

  if (error) {
    return <div className="error">{error}</div>;
  }

  if (properties.length === 0) {
    return (
      <div className="card">
        <h3>No properties found</h3>
        <p>Try adjusting your search criteria or check back later for new listings.</p>
      </div>
    );
  }

  return (
    <div className="property-grid">
      {properties.map((property) => (
        <PropertyCard
          key={property.id}
          property={property}
          onViewDetails={onViewDetails}
        />
      ))}
    </div>
  );
};

export default PropertyList;
