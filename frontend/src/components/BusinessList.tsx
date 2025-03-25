import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import api from '@/services/api';

interface Business {
  businessId: number;
  name: string;
  address: string;
  city: string;
  phoneNumber: string;
  categoryId: number;
  category: {
    name: string;
  };
}

const BusinessList: React.FC = () => {
  const [businesses, setBusinesses] = useState<Business[]>([]);

  useEffect(() => {
    const fetchBusinesses = async () => {
      try {
        const response = await api.get('/Business');
        setBusinesses(response.data);
      } catch (error) {
        console.error('İşletmeler yüklenirken hata oluştu:', error);
      }
    };

    fetchBusinesses();
  }, []);

  return (
    <div className="max-w-7xl mx-auto p-4">
      <div className="flex justify-between items-center mb-4">
        <h2 className="text-2xl font-bold">İşletmeler</h2>
        <Link
          to="/businesses/new"
          className="bg-indigo-600 text-white px-4 py-2 rounded-md hover:bg-indigo-700"
        >
          Yeni İşletme Ekle
        </Link>
      </div>
      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
        {businesses.map(business => (
          <div
            key={business.businessId}
            className="bg-white p-4 rounded-lg shadow-md hover:shadow-lg transition-shadow"
          >
            <h3 className="text-xl font-semibold mb-2">{business.name}</h3>
            <p className="text-gray-600 mb-2">{business.category.name}</p>
            <p className="text-gray-600 mb-2">{business.address}</p>
            <p className="text-gray-600 mb-2">{business.city}</p>
            <p className="text-gray-600">{business.phoneNumber}</p>
          </div>
        ))}
      </div>
    </div>
  );
};

export default BusinessList; 