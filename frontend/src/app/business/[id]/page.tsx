'use client';

import React, { useEffect, useState } from 'react';
import { useParams } from 'next/navigation';
import { Business } from '../../../services/api';
import { businessService } from '../../../services/api';
import { FaMapMarkerAlt, FaPhone, FaEnvelope, FaTag, FaCalendarAlt, FaClock } from 'react-icons/fa';

export default function BusinessDetailPage() {
  const params = useParams();
  const [business, setBusiness] = useState<Business | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchBusiness = async () => {
      try {
        const data = await businessService.getById(Number(params.id));
        setBusiness(data);
      } catch (error) {
        console.error('İşletme detayları yüklenirken hata oluştu:', error);
        setError('İşletme detayları yüklenirken bir hata oluştu. Lütfen sayfayı yenileyin.');
      } finally {
        setLoading(false);
      }
    };

    fetchBusiness();
  }, [params.id]);

  if (loading) {
    return (
      <div className="min-h-screen bg-gray-900 flex items-center justify-center">
        <div className="animate-spin rounded-full h-12 w-12 border-t-2 border-b-2 border-indigo-500"></div>
      </div>
    );
  }

  if (error || !business) {
    return (
      <div className="min-h-screen bg-gray-900 flex items-center justify-center">
        <div className="text-center">
          <h2 className="text-2xl font-semibold text-red-400 mb-4">Hata</h2>
          <p className="text-gray-400">{error || 'İşletme bulunamadı.'}</p>
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
    <div className="min-h-screen bg-gray-900">
      {/* Hero Section */}
      <div className="relative bg-gradient-to-br from-gray-900 via-indigo-900 to-purple-900 py-24">
        <div className="absolute inset-0 bg-[url('/grid.svg')] bg-center [mask-image:linear-gradient(180deg,white,rgba(255,255,255,0))]" />
        <div className="container mx-auto px-4 relative">
          <h1 className="text-5xl font-bold text-white mb-6 font-display bg-clip-text text-transparent bg-gradient-to-r from-indigo-400 to-purple-400">
            {business.name}
          </h1>
          <div className="flex items-center space-x-4">
            <div className="flex items-center bg-gray-800/50 px-6 py-3 rounded-full backdrop-blur-sm border border-gray-700/50 hover:border-indigo-500/50 transition-all duration-300 group">
              <FaTag className="w-6 h-6 text-indigo-400 mr-3 group-hover:scale-110 transition-transform duration-300" />
              <span className="font-semibold text-lg">{business.category?.name}</span>
            </div>
            <div className="flex items-center bg-gray-800/50 px-6 py-3 rounded-full backdrop-blur-sm border border-gray-700/50 hover:border-green-500/50 transition-all duration-300 group">
              <FaPhone className="w-6 h-6 text-green-400 mr-3 group-hover:scale-110 transition-transform duration-300" />
              <span className="font-semibold text-lg">{business.phone}</span>
            </div>
          </div>
        </div>
      </div>

      {/* Ana İçerik */}
      <div className="container mx-auto px-4 py-16">
        <div className="grid grid-cols-1 lg:grid-cols-3 gap-12">
          {/* Sol Kolon - İşletme Bilgileri */}
          <div className="lg:col-span-2">
            <div className="bg-gray-800/80 backdrop-blur-lg rounded-2xl p-8 shadow-xl border border-gray-700">
              <h2 className="text-2xl font-bold text-white mb-6">İşletme Hakkında</h2>
              <p className="text-gray-300 mb-8">{business.description}</p>
              
              <div className="space-y-6">
                <div className="flex items-start">
                  <FaMapMarkerAlt className="w-6 h-6 text-indigo-400 mt-1 mr-4" />
                  <div>
                    <h3 className="text-lg font-semibold text-white mb-2">Adres</h3>
                    <p className="text-gray-300">{business.address}, {business.city}</p>
                  </div>
                </div>
                
                <div className="flex items-start">
                  <FaPhone className="w-6 h-6 text-indigo-400 mt-1 mr-4" />
                  <div>
                    <h3 className="text-lg font-semibold text-white mb-2">İletişim</h3>
                    <p className="text-gray-300">{business.phone}</p>
                    <p className="text-gray-300">{business.email}</p>
                  </div>
                </div>
              </div>
            </div>

            {/* Aktif Kampanyalar */}
            {business.campaigns && business.campaigns.length > 0 && (
              <div className="mt-12">
                <h2 className="text-2xl font-bold text-white mb-6">Aktif Kampanyalar</h2>
                <div className="space-y-6">
                  {business.campaigns.map((campaign) => (
                    <div
                      key={campaign.campaignId}
                      className="bg-gray-800/80 backdrop-blur-lg rounded-2xl p-6 shadow-xl border border-gray-700 hover:shadow-2xl transition-all duration-300"
                    >
                      <div className="flex items-center justify-between mb-4">
                        <h3 className="text-xl font-bold text-white">{campaign.title}</h3>
                        <span className="inline-flex items-center px-4 py-2 rounded-full text-sm font-semibold bg-yellow-900/50 text-yellow-400 border border-yellow-800">
                          %{campaign.discountRate} İndirim
                        </span>
                      </div>
                      <p className="text-gray-300 mb-4">{campaign.description}</p>
                      <div className="flex items-center text-gray-400">
                        <FaCalendarAlt className="w-5 h-5 mr-2" />
                        <span>
                          {new Date(campaign.startDate).toLocaleDateString()} - {new Date(campaign.endDate).toLocaleDateString()}
                        </span>
                      </div>
                    </div>
                  ))}
                </div>
              </div>
            )}
          </div>

          {/* Sağ Kolon - Harita */}
          <div className="lg:col-span-1">
            <div className="bg-gray-800/80 backdrop-blur-lg rounded-2xl p-6 shadow-xl border border-gray-700 sticky top-8">
              <h2 className="text-xl font-bold text-white mb-4">Konum</h2>
              <div className="aspect-square rounded-xl overflow-hidden">
                <iframe
                  src={`https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3019.0!2d0!3d0!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x0%3A0x0!2zMzXCsDQ4JzQ4LjAiTiA0MMKwNDgnNDguMCJF!5e0!3m2!1str!2str!4v1234567890!5m2!1str!2str`}
                  width="100%"
                  height="100%"
                  style={{ border: 0 }}
                  allowFullScreen
                  loading="lazy"
                  referrerPolicy="no-referrer-when-downgrade"
                  className="rounded-xl"
                />
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
} 