import React from 'react';
import { Property } from '../types/Property';

interface PropertyCardProps {
  property: Property;
  onViewDetails: (property: Property) => void;
}

const PropertyCard: React.FC<PropertyCardProps> = ({ property, onViewDetails }) => {
  const formatPrice = (price: number) => {
    return new Intl.NumberFormat('en-US', {
      style: 'currency',
      currency: 'USD',
      minimumFractionDigits: 0,
      maximumFractionDigits: 0,
    }).format(price);
  };

  return (
    <div className="property-card">
      <img 
        src={property.imageUrl || 'https://via.placeholder.com/300x200?text=No+Image'} 
        alt={property.name}
        className="property-image"
        onError={(e) => {
          const target = e.target as HTMLImageElement;
          target.src = 'https://via.placeholder.com/300x200?text=No+Image';
        }}
      />
      <div className="property-info">
        <div className="property-price">{formatPrice(property.price)}</div>
        <div className="property-name">{property.name}</div>
        <div className="property-address">{property.address}</div>
  <div style={{ fontSize: '12px', color: '#666' }}>Code: {property.codeInternal} • Year: {property.year}</div>
        <button 
          className="btn btn-primary" 
          onClick={() => onViewDetails(property)}
          style={{ width: '100%' }}
        >
          View Details
        </button>
      </div>
    </div>
  );
};

export default PropertyCard;
