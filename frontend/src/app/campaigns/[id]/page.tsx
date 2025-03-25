'use client';

import React, { useEffect, useState } from 'react';
import { useParams } from 'next/navigation';
import { Campaign } from '../../../services/api';
import { campaignService } from '../../../services/api';
import { FaPhone, FaEnvelope, FaMapMarkerAlt, FaStore, FaPercent, FaCalendarAlt, FaArrowLeft } from 'react-icons/fa';
import Link from 'next/link';

export default function CampaignDetailPage() {
  const params = useParams();
  const [campaign, setCampaign] = useState<Campaign | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchCampaign = async () => {
      try {
        const data = await campaignService.getById(Number(params.id));
        setCampaign(data);
      } catch (error) {
        setError('Kampanya bilgileri yüklenirken bir hata oluştu. Lütfen sayfayı yenileyin.');
      } finally {
        setLoading(false);
      }
    };

    fetchCampaign();
  }, [params.id]);

  if (loading) {
    return (
      <div className="min-h-screen flex items-center justify-center bg-gray-900">
        <div className="relative">
          <div className="w-24 h-24 border-4 border-indigo-500 border-t-transparent rounded-full animate-spin" />
          <div className="absolute inset-0 flex items-center justify-center">
            <div className="w-16 h-16 border-4 border-purple-500 border-t-transparent rounded-full animate-spin-reverse" />
          </div>
        </div>
      </div>
    );
  }

  if (error || !campaign) {
    return (
      <div className="min-h-screen flex items-center justify-center bg-gray-900">
        <div className="text-center p-12 bg-gray-800/80 backdrop-blur-lg rounded-3xl shadow-2xl max-w-lg mx-4 border border-gray-700">
          <div className="w-20 h-20 mx-auto mb-6 rounded-full bg-red-900/50 flex items-center justify-center">
            <svg className="w-10 h-10 text-red-500" fill="none" stroke="currentColor" viewBox="0 0 24 24">
              <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M12 9v2m0 4h.01m-6.938 4h13.856c1.54 0 2.502-1.667 1.732-3L13.732 4c-.77-1.333-2.694-1.333-3.464 0L3.34 16c-.77 1.333.192 3 1.732 3z" />
            </svg>
          </div>
          <h2 className="text-3xl font-bold text-red-500 mb-4">Hata</h2>
          <p className="text-gray-300 mb-8">{error || 'Kampanya bulunamadı.'}</p>
          <button
            onClick={() => window.location.reload()}
            className="px-8 py-4 bg-gradient-to-r from-red-600 to-orange-600 text-white rounded-full font-semibold hover:shadow-xl transition-all duration-300 hover:scale-105"
          >
            Sayfayı Yenile
          </button>
        </div>
      </div>
    );
  }

  return (
    <div className="min-h-screen bg-gray-900 pt-20">
      {/* Hero Section */}
      <div className="relative overflow-hidden">
        <div className="absolute inset-0">
          <div className="absolute inset-0 bg-gradient-to-br from-gray-900 via-indigo-900 to-purple-900 opacity-90" />
          <div className="absolute inset-0 bg-[url('/grid.svg')] bg-center [mask-image:linear-gradient(180deg,white,rgba(255,255,255,0))]" />
        </div>
        <div className="relative container mx-auto px-4 py-16">
          <div className="max-w-4xl mx-auto">
            <div className="flex items-center space-x-4 mb-6">
              <Link
                href="/campaigns"
                className="text-gray-400 hover:text-white transition-colors duration-300 flex items-center"
              >
                <FaArrowLeft className="w-5 h-5 mr-2" />
                Kampanyalar
              </Link>
              <span className="text-gray-600">/</span>
              <span className="text-white">{campaign.title}</span>
            </div>
            <h1 className="text-5xl md:text-6xl font-bold text-white mb-8 leading-tight font-display">
              {campaign.title}
            </h1>
            <div className="flex items-center space-x-4 text-gray-300">
              <div className="flex items-center">
                <FaStore className="w-5 h-5 text-indigo-400 mr-2" />
                <span>{campaign.business.name}</span>
              </div>
              <span>•</span>
              <div className="flex items-center bg-yellow-900/50 px-4 py-2 rounded-full border border-yellow-800">
                <FaPercent className="w-5 h-5 text-yellow-400 mr-2" />
                <span className="text-yellow-400 font-semibold">%{campaign.discountRate} İndirim</span>
              </div>
            </div>
          </div>
        </div>
      </div>

      {/* Ana İçerik */}
      <div className="container mx-auto px-4 py-16">
        <div className="grid grid-cols-1 lg:grid-cols-3 gap-8">
          {/* Sol Kolon - Kampanya Detayları */}
          <div className="lg:col-span-2">
            <div className="bg-gray-800/80 backdrop-blur-lg rounded-2xl p-8 shadow-xl border border-gray-700 mb-8">
              <h2 className="text-2xl font-bold text-white mb-6 font-display">Kampanya Detayları</h2>
              <p className="text-gray-300 leading-relaxed mb-8">
                {campaign.description}
              </p>
              
              <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
                <div className="flex items-start space-x-4">
                  <div className="w-12 h-12 bg-indigo-900/50 rounded-xl flex items-center justify-center flex-shrink-0">
                    <FaCalendarAlt className="w-6 h-6 text-indigo-400" />
                  </div>
                  <div>
                    <h3 className="text-lg font-semibold text-white mb-1">Başlangıç Tarihi</h3>
                    <p className="text-gray-400">
                      {new Date(campaign.startDate).toLocaleDateString('tr-TR')}
                    </p>
                  </div>
                </div>

                <div className="flex items-start space-x-4">
                  <div className="w-12 h-12 bg-indigo-900/50 rounded-xl flex items-center justify-center flex-shrink-0">
                    <FaCalendarAlt className="w-6 h-6 text-indigo-400" />
                  </div>
                  <div>
                    <h3 className="text-lg font-semibold text-white mb-1">Bitiş Tarihi</h3>
                    <p className="text-gray-400">
                      {new Date(campaign.endDate).toLocaleDateString('tr-TR')}
                    </p>
                  </div>
                </div>

                <div className="flex items-start space-x-4">
                  <div className="w-12 h-12 bg-indigo-900/50 rounded-xl flex items-center justify-center flex-shrink-0">
                    <FaPercent className="w-6 h-6 text-indigo-400" />
                  </div>
                  <div>
                    <h3 className="text-lg font-semibold text-white mb-1">İndirim Oranı</h3>
                    <p className="text-gray-400">
                      %{campaign.discountRate}
                    </p>
                  </div>
                </div>

                <div className="flex items-start space-x-4">
                  <div className="w-12 h-12 bg-indigo-900/50 rounded-xl flex items-center justify-center flex-shrink-0">
                    <FaStore className="w-6 h-6 text-indigo-400" />
                  </div>
                  <div>
                    <h3 className="text-lg font-semibold text-white mb-1">İşletme</h3>
                    <Link
                      href={`/businesses/${campaign.business.businessId}`}
                      className="text-gray-400 hover:text-indigo-400 transition-colors duration-300"
                    >
                      {campaign.business.name}
                    </Link>
                  </div>
                </div>
              </div>
            </div>

            {/* Kampanya Şartları */}
            {campaign.terms && (
              <div className="bg-gray-800/80 backdrop-blur-lg rounded-2xl p-8 shadow-xl border border-gray-700">
                <h2 className="text-2xl font-bold text-white mb-6 font-display">Kampanya Şartları</h2>
                <div className="prose prose-invert max-w-none">
                  {campaign.terms}
                </div>
              </div>
            )}
          </div>

          {/* Sağ Kolon - İşletme Bilgileri ve Harita */}
          <div className="lg:col-span-1">
            <div className="bg-gray-800/80 backdrop-blur-lg rounded-2xl p-8 shadow-xl border border-gray-700 sticky top-24">
              <h2 className="text-2xl font-bold text-white mb-6 font-display">İşletme Bilgileri</h2>
              
              <div className="aspect-w-16 aspect-h-9 rounded-xl overflow-hidden mb-6">
                <iframe
                  src={`https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3019.0!2d${campaign.business.longitude}!3d${campaign.business.latitude}!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x0%3A0x0!2z!5e0!3m2!1str!2str!4v1234567890!5m2!1str!2str`}
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