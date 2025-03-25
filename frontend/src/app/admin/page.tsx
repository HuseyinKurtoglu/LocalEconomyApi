'use client';

import React, { useEffect, useState } from 'react';
import { Business, Campaign } from '../../services/api';
import { businessService, campaignService } from '../../services/api';
import { FaStore, FaPercent, FaEdit, FaTrash } from 'react-icons/fa';

export default function AdminPage() {
  const [businesses, setBusinesses] = useState<Business[]>([]);
  const [campaigns, setCampaigns] = useState<Campaign[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [activeTab, setActiveTab] = useState<'businesses' | 'campaigns'>('businesses');

  useEffect(() => {
    const fetchData = async () => {
      try {
        const [businessesData, campaignsData] = await Promise.all([
          businessService.getAll(),
          campaignService.getAll()
        ]);
        setBusinesses(businessesData);
        setCampaigns(campaignsData);
      } catch (error) {
        console.error('Veri yüklenirken hata:', error);
        setError('Veriler yüklenirken bir hata oluştu. Lütfen sayfayı yenileyin.');
      } finally {
        setLoading(false);
      }
    };

    fetchData();
  }, []);

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
    <div>
      {/* İstatistikler */}
      <div className="grid grid-cols-1 md:grid-cols-2 gap-6 mb-8">
        <div className="bg-white rounded-lg shadow-sm p-6 border border-gray-200">
          <div className="flex items-center space-x-4">
            <div className="w-12 h-12 bg-indigo-50 rounded-xl flex items-center justify-center">
              <FaStore className="w-6 h-6 text-indigo-600" />
            </div>
            <div>
              <h3 className="text-2xl font-bold text-gray-900">{businesses.length}</h3>
              <p className="text-gray-600">Toplam İşletme</p>
            </div>
          </div>
        </div>

        <div className="bg-white rounded-lg shadow-sm p-6 border border-gray-200">
          <div className="flex items-center space-x-4">
            <div className="w-12 h-12 bg-yellow-50 rounded-xl flex items-center justify-center">
              <FaPercent className="w-6 h-6 text-yellow-600" />
            </div>
            <div>
              <h3 className="text-2xl font-bold text-gray-900">{campaigns.length}</h3>
              <p className="text-gray-600">Aktif Kampanya</p>
            </div>
          </div>
        </div>
      </div>

      {/* Tab Menüsü */}
      <div className="flex space-x-4 mb-8">
        <button
          onClick={() => setActiveTab('businesses')}
          className={`px-6 py-3 rounded-lg text-sm font-semibold transition-all duration-300 ${
            activeTab === 'businesses'
              ? 'bg-indigo-600 text-white'
              : 'bg-white text-gray-600 hover:bg-gray-50 border border-gray-200'
          }`}
        >
          İşletmeler
        </button>
        <button
          onClick={() => setActiveTab('campaigns')}
          className={`px-6 py-3 rounded-lg text-sm font-semibold transition-all duration-300 ${
            activeTab === 'campaigns'
              ? 'bg-indigo-600 text-white'
              : 'bg-white text-gray-600 hover:bg-gray-50 border border-gray-200'
          }`}
        >
          Kampanyalar
        </button>
      </div>

      {/* İçerik Alanı */}
      <div className="bg-white rounded-lg shadow-sm p-6 border border-gray-200">
        <div className="flex justify-between items-center mb-6">
          <h2 className="text-xl font-semibold text-gray-900">
            {activeTab === 'businesses' ? 'İşletmeler' : 'Kampanyalar'}
          </h2>
        </div>

        <div className="overflow-x-auto">
          <table className="w-full">
            <thead>
              <tr className="text-left text-gray-500 text-sm">
                <th className="pb-4">ID</th>
                <th className="pb-4">Ad</th>
                <th className="pb-4">Şehir</th>
                <th className="pb-4">İşlemler</th>
              </tr>
            </thead>
            <tbody>
              {activeTab === 'businesses' && businesses.map((business) => (
                <tr key={business.businessId} className="border-t border-gray-100">
                  <td className="py-4 text-gray-900">{business.businessId}</td>
                  <td className="py-4 text-gray-900">{business.name}</td>
                  <td className="py-4 text-gray-900">{business.city}</td>
                  <td className="py-4">
                    <div className="flex space-x-2">
                      <button className="p-2 text-indigo-600 hover:text-indigo-700">
                        <FaEdit className="w-4 h-4" />
                      </button>
                      <button className="p-2 text-red-600 hover:text-red-700">
                        <FaTrash className="w-4 h-4" />
                      </button>
                    </div>
                  </td>
                </tr>
              ))}
              {activeTab === 'campaigns' && campaigns.map((campaign) => (
                <tr key={campaign.campaignId} className="border-t border-gray-100">
                  <td className="py-4 text-gray-900">{campaign.campaignId}</td>
                  <td className="py-4 text-gray-900">{campaign.title}</td>
                  <td className="py-4 text-gray-900">{campaign.business?.city}</td>
                  <td className="py-4">
                    <div className="flex space-x-2">
                      <button className="p-2 text-indigo-600 hover:text-indigo-700">
                        <FaEdit className="w-4 h-4" />
                      </button>
                      <button className="p-2 text-red-600 hover:text-red-700">
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