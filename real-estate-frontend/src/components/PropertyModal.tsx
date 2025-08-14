import React from 'react';
import { Property } from '../types/Property';

interface PropertyModalProps {
  property: Property | null;
  isOpen: boolean;
  onClose: () => void;
}

const PropertyModal: React.FC<PropertyModalProps> = ({ property, isOpen, onClose }) => {
  if (!isOpen || !property) return null;

  const formatPrice = (price: number) => {
    return new Intl.NumberFormat('en-US', {
      style: 'currency',
      currency: 'USD',
      minimumFractionDigits: 0,
      maximumFractionDigits: 0,
    }).format(price);
  };

  return (
    <div 
      style={{
        position: 'fixed',
        top: 0,
        left: 0,
        right: 0,
        bottom: 0,
        backgroundColor: 'rgba(0, 0, 0, 0.5)',
        display: 'flex',
        justifyContent: 'center',
        alignItems: 'center',
        zIndex: 1000
      }}
      onClick={onClose}
    >
      <div 
        className="card"
        style={{
          maxWidth: '600px',
          maxHeight: '80vh',
          overflow: 'auto',
          position: 'relative'
        }}
        onClick={(e) => e.stopPropagation()}
      >
        <button
          onClick={onClose}
          style={{
            position: 'absolute',
            top: '10px',
            right: '10px',
            background: 'none',
            border: 'none',
            fontSize: '24px',
            cursor: 'pointer',
            color: '#666'
          }}
        >
          ×
        </button>
        
        <img 
          src={property.imageUrl || 'https://via.placeholder.com/600x300?text=No+Image'} 
          alt={property.name}
          style={{
            width: '100%',
            height: '300px',
            objectFit: 'cover',
            borderRadius: '8px 8px 0 0',
            marginBottom: '20px'
          }}
          onError={(e) => {
            const target = e.target as HTMLImageElement;
            target.src = 'https://via.placeholder.com/600x300?text=No+Image';
          }}
        />
        
        <div className="property-price" style={{ fontSize: '32px' }}>
          {formatPrice(property.price)}
        </div>
        
        <h2 style={{ marginBottom: '15px' }}>{property.name}</h2>
        
        <div style={{ marginBottom: '20px' }}>
          <strong>Address:</strong> {property.address}
        </div>
        
        <div style={{ marginBottom: '20px' }}>
          <strong>Owner ID:</strong> {property.idOwner}
        </div>
        
        <div style={{ marginBottom: '20px' }}>
          <strong>Property ID:</strong> {property.id}
        </div>
      </div>
    </div>
  );
};

export default PropertyModal;
