'use client';

import React, { useEffect, useState } from 'react';
import { Business, Category } from '@/types';
import { businessService, categoryService } from '@/services/api';
import { FaEdit, FaTrash, FaPlus } from 'react-icons/fa';
import { toast } from 'react-hot-toast';

export default function BusinessesPage() {
  const [businesses, setBusinesses] = useState<Business[]>([]);
  const [categories, setCategories] = useState<Category[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [showAddForm, setShowAddForm] = useState(false);
  const [newBusiness, setNewBusiness] = useState({
    name: '',
    phone: '',
    email: '',
    city: '',
    address: '',
    description: '',
    imageUrl: '',
    categoryId: ''
  });

  useEffect(() => {
    fetchData();
  }, []);

  const fetchData = async () => {
    try {
      const [businessesData, categoriesData] = await Promise.all([
        businessService.getAll(),
        categoryService.getAll()
      ]);
      setBusinesses(businessesData);
      setCategories(categoriesData);
    } catch (err) {
      setError('Veriler yüklenirken bir hata oluştu');
      console.error('Veri yükleme hatası:', err);
    } finally {
      setLoading(false);
    }
  };

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    const { name, value } = e.target;
    setNewBusiness(prev => ({
      ...prev,
      [name]: value
    }));
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      if (!newBusiness.name || !newBusiness.categoryId) {
        toast.error('İşletme adı ve kategori seçimi zorunludur');
        return;
      }

      const businessData = {
        name: newBusiness.name,
        phone: newBusiness.phone || null,
        email: newBusiness.email || null,
        city: newBusiness.city || null,
        address: newBusiness.address || null,
        description: newBusiness.description || null,
        imageUrl: newBusiness.imageUrl || null,
        categoryId: parseInt(newBusiness.categoryId),
        isDeleted: false,
        createdDate: new Date().toISOString()
      };

      console.log('Gönderilen veri:', businessData);
      await businessService.create(businessData);
      toast.success('İşletme başarıyla eklendi');
      setShowAddForm(false);
      setNewBusiness({
        name: '',
        phone: '',
        email: '',
        city: '',
        address: '',
        description: '',
        imageUrl: '',
        categoryId: ''
      });
      fetchData();
    } catch (err: any) {
      console.error('İşletme ekleme hatası:', err);
      if (err.response?.data?.errors) {
        const errorMessages = Object.values(err.response.data.errors).flat();
        toast.error(`Hata: ${errorMessages.join(', ')}`);
      } else {
        toast.error('İşletme eklenirken bir hata oluştu');
      }
    }
  };

  const handleDelete = async (id: number) => {
    if (window.confirm('Bu işletmeyi silmek istediğinizden emin misiniz?')) {
      try {
        await businessService.delete(id);
        toast.success('İşletme başarıyla silindi');
        fetchData();
      } catch (err) {
        toast.error('İşletme silinirken bir hata oluştu');
        console.error('İşletme silme hatası:', err);
      }
    }
  };

  if (loading) {
    return (
      <div className="min-h-screen flex items-center justify-center">
        <div className="animate-spin rounded-full h-12 w-12 border-t-2 border-b-2 border-indigo-500"></div>
      </div>
    );
  }

  if (error) {
    return (
      <div className="min-h-screen flex items-center justify-center">
        <div className="text-center">
          <h2 className="text-2xl font-semibold text-red-600 mb-4">Hata</h2>
          <p className="text-gray-600">{error}</p>
          <button
            onClick={() => window.location.reload()}
            className="mt-4 px-4 py-2 bg-indigo-600 text-white rounded-lg hover:bg-indigo-700 transition-colors"
          >
            Sayfayı Yenile
          </button>
        </div>
      </div>
    );
  }

  return (
    <div className="p-6">
      <div className="flex justify-between items-center mb-8">
        <h1 className="text-2xl font-bold text-gray-900">İşletmeler</h1>
        <button
          onClick={() => setShowAddForm(true)}
          className="px-4 py-2 bg-indigo-600 text-white rounded-lg hover:bg-indigo-700 transition-colors flex items-center"
        >
          <FaPlus className="mr-2" />
          Yeni İşletme Ekle
        </button>
      </div>

      {/* İşletme Ekleme Formu */}
      {showAddForm && (
        <div className="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center p-4 z-50">
          <div className="bg-white rounded-lg shadow-xl max-w-2xl w-full max-h-[90vh] overflow-y-auto">
            <div className="p-6">
              <div className="flex justify-between items-center mb-6">
                <h2 className="text-xl font-semibold text-gray-900">Yeni İşletme Ekle</h2>
                <button
                  onClick={() => setShowAddForm(false)}
                  className="text-gray-400 hover:text-gray-500"
                >
                  <svg className="h-6 w-6" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M6 18L18 6M6 6l12 12" />
                  </svg>
                </button>
              </div>
              <form onSubmit={handleSubmit} className="space-y-4">
                <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                  <div>
                    <label className="block text-sm font-medium text-gray-700 mb-1">İşletme Adı</label>
                    <input
                      type="text"
                      name="name"
                      value={newBusiness.name}
                      onChange={handleInputChange}
                      className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-blue-500 focus:ring-blue-500"
                      required
                    />
                  </div>
                  <div>
                    <label className="block text-sm font-medium text-gray-700 mb-1">Telefon</label>
                    <input
                      type="tel"
                      name="phone"
                      value={newBusiness.phone}
                      onChange={handleInputChange}
                      className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-blue-500 focus:ring-blue-500"
                      required
                    />
                  </div>
                  <div>
                    <label className="block text-sm font-medium text-gray-700 mb-1">E-posta</label>
                    <input
                      type="email"
                      name="email"
                      value={newBusiness.email}
                      onChange={handleInputChange}
                      className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-blue-500 focus:ring-blue-500"
                      required
                    />
                  </div>
                  <div>
                    <label className="block text-sm font-medium text-gray-700 mb-1">Şehir</label>
                    <input
                      type="text"
                      name="city"
                      value={newBusiness.city}
                      onChange={handleInputChange}
                      className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-blue-500 focus:ring-blue-500"
                      required
                    />
                  </div>
                </div>
                <div>
                  <label className="block text-sm font-medium text-gray-700 mb-1">Adres</label>
                  <textarea
                    name="address"
                    value={newBusiness.address}
                    onChange={handleInputChange}
                    rows={2}
                    className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-blue-500 focus:ring-blue-500"
                    required
                  />
                </div>
                <div>
                  <label className="block text-sm font-medium text-gray-700 mb-1">Açıklama</label>
                  <textarea
                    name="description"
                    value={newBusiness.description}
                    onChange={handleInputChange}
                    rows={3}
                    className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-blue-500 focus:ring-blue-500"
                    required
                  />
                </div>
                <div>
                  <label className="block text-sm font-medium text-gray-700 mb-1">Resim URL</label>
                  <input
                    type="url"
                    name="imageUrl"
                    value={newBusiness.imageUrl}
                    onChange={handleInputChange}
                    className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-blue-500 focus:ring-blue-500"
                  />
                </div>
                <div>
                  <label className="block text-sm font-medium text-gray-700 mb-1">Kategori</label>
                  <select
                    value={newBusiness.categoryId}
                    onChange={(e) => setNewBusiness({ ...newBusiness, categoryId: e.target.value })}
                    className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-blue-500 focus:ring-blue-500"
                    required
                  >
                    <option key="default" value="">Kategori Seçin</option>
                    {categories.map((category) => (
                      <option key={category.categoryId} value={category.categoryId}>
                        {category.name}
                      </option>
                    ))}
                  </select>
                </div>
                <div className="flex justify-end space-x-4 mt-6">
                  <button
                    type="button"
                    onClick={() => setShowAddForm(false)}
                    className="px-4 py-2 border border-gray-300 text-gray-700 rounded-lg hover:bg-gray-50"
                  >
                    İptal
                  </button>
                  <button
                    type="submit"
                    className="px-4 py-2 bg-blue-500 text-white rounded-lg hover:bg-blue-600 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:ring-offset-2"
                  >
                    Kaydet
                  </button>
                </div>
              </form>
            </div>
          </div>
        </div>
      )}

      {/* İşletme Listesi */}
      <div className="bg-white rounded-lg shadow-sm p-6 border border-gray-200">
        <div className="overflow-x-auto">
          <table className="min-w-full divide-y divide-gray-200">
            <thead className="bg-gray-50">
              <tr>
                <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">ID</th>
                <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Ad</th>
                <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Şehir</th>
                <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Kategori</th>
                <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">İşlemler</th>
              </tr>
            </thead>
            <tbody className="bg-white divide-y divide-gray-200">
              {businesses.map((business) => (
                <tr key={business.businessId}>
                  <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-900">{business.businessId}</td>
                  <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-900">{business.name}</td>
                  <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-900">{business.city}</td>
                  <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-900">{categories.find(c => c.id === business.categoryId)?.name || '-'}</td>
                  <td className="px-6 py-4 whitespace-nowrap text-sm font-medium">
                    <div className="flex space-x-2">
                      <button className="text-indigo-600 hover:text-indigo-900">
                        <FaEdit className="w-4 h-4" />
                      </button>
                      <button 
                        onClick={() => handleDelete(business.businessId)}
                        className="text-red-600 hover:text-red-900"
                      >
                        <FaTrash className="w-4 h-4" />
                      </button>
                    </div>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      </div>
    </div>
  );
} 