import React, { useState, useEffect } from 'react';
import PropertyFilters from './components/PropertyFilters';
import PropertyList from './components/PropertyList';
import PropertyModal from './components/PropertyModal';
import { Property, PropertyFilter } from './types/Property';
import propertyService from './services/propertyService';

function App() {
  const [properties, setProperties] = useState<Property[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [selectedProperty, setSelectedProperty] = useState<Property | null>(null);
  const [isModalOpen, setIsModalOpen] = useState(false);

  useEffect(() => {
    loadProperties();
  }, []);

  const loadProperties = async () => {
    try {
      setLoading(true);
      setError(null);
      const data = await propertyService.getAllProperties();
      setProperties(data);
    } catch (err) {
      setError('Failed to load properties. Please try again later.');
      console.error('Error loading properties:', err);
    } finally {
      setLoading(false);
    }
  };

  const handleFilterChange = async (filter: PropertyFilter) => {
    try {
      setLoading(true);
      setError(null);
      const data = await propertyService.getFilteredProperties(filter);
      setProperties(data);
    } catch (err) {
      setError('Failed to filter properties. Please try again.');
      console.error('Error filtering properties:', err);
    } finally {
      setLoading(false);
    }
  };

  const handleClearFilters = () => {
    loadProperties();
  };

  const handleViewDetails = (property: Property) => {
    setSelectedProperty(property);
    setIsModalOpen(true);
  };

  const closeModal = () => {
    setIsModalOpen(false);
    setSelectedProperty(null);
  };

  return (
    <div className="App">
      <header className="header">
        <div className="container">
          <h1>🏠 Real Estate Properties</h1>
        </div>
      </header>

      <main className="container">
        <PropertyFilters 
          onFilterChange={handleFilterChange}
          onClearFilters={handleClearFilters}
        />
        
        <PropertyList
          properties={properties}
          onViewDetails={handleViewDetails}
          loading={loading}
          error={error}
        />
      </main>

      <PropertyModal
        property={selectedProperty}
        isOpen={isModalOpen}
        onClose={closeModal}
      />
    </div>
  );
}

export default App;
