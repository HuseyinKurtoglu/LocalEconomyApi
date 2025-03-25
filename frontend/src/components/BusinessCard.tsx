'use client';

import React from 'react';
import Link from 'next/link';
import { Business } from '../services/api';
import { FaMapMarkerAlt, FaPhone, FaEnvelope, FaTag, FaCalendarAlt } from 'react-icons/fa';

interface BusinessCardProps {
  business: Business;
}

export default function BusinessCard({ business }: BusinessCardProps) {
  return (
    <div className="group bg-gray-800/80 backdrop-blur-lg rounded-2xl shadow-xl overflow-hidden border border-gray-700 hover:shadow-2xl transition-all duration-500 hover:-translate-y-2">
      <div className="relative h-56">
        <div className="absolute inset-0 bg-gradient-to-br from-gray-900 via-indigo-900 to-purple-900 opacity-90" />
        <div className="absolute inset-0 bg-[url('/grid.svg')] bg-center [mask-image:linear-gradient(180deg,white,rgba(255,255,255,0))]" />
        <div className="absolute inset-0 flex items-center justify-center">
          <h2 className="text-3xl font-bold text-white text-center px-4 group-hover:scale-105 transition-transform duration-500 font-display">
            {business.name}
          </h2>
        </div>
      </div>

      <div className="p-6">
        <p className="text-gray-300 mb-6 line-clamp-3 group-hover:text-white transition-colors duration-300">
          {business.description}
        </p>
        
        <div className="space-y-3 mb-6">
          <div className="flex items-center text-gray-400 group-hover:text-gray-300 transition-colors duration-300">
            <FaMapMarkerAlt className="w-5 h-5 text-indigo-400 mr-3" />
            <span className="text-sm">{business.address}, {business.city}</span>
          </div>
          <div className="flex items-center text-gray-400 group-hover:text-gray-300 transition-colors duration-300">
            <FaPhone className="w-5 h-5 text-indigo-400 mr-3" />
            <span className="text-sm">{business.phone}</span>
          </div>
          <div className="flex items-center text-gray-400 group-hover:text-gray-300 transition-colors duration-300">
            <FaEnvelope className="w-5 h-5 text-indigo-400 mr-3" />
            <span className="text-sm">{business.email}</span>
          </div>
        </div>

        {business.campaigns && business.campaigns.length > 0 && (
          <div className="mb-6">
            <h3 className="text-lg font-bold text-white mb-4 flex items-center">
              <FaTag className="w-5 h-5 text-yellow-400 mr-2" />
              Aktif Kampanyalar
            </h3>
            <div className="space-y-3">
              {business.campaigns.map((campaign) => (
                <div
                  key={campaign.campaignId}
                  className="bg-gray-700/50 p-4 rounded-xl border border-gray-600 hover:scale-[1.02] transition-transform duration-200 group-hover:shadow-md"
                >
                  <h4 className="font-bold text-yellow-400 mb-2">{campaign.title}</h4>
                  <p className="text-sm text-gray-300 mb-3 line-clamp-2">{campaign.description}</p>
                  <div className="flex flex-col sm:flex-row justify-between items-start sm:items-center gap-2">
                    <span className="inline-flex items-center px-3 py-1 rounded-full text-sm font-semibold bg-yellow-900/50 text-yellow-400 border border-yellow-800">
                      %{campaign.discountRate} İndirim
                    </span>
                    <span className="inline-flex items-center text-sm text-gray-400">
                      <FaCalendarAlt className="w-4 h-4 mr-2" />
                      {new Date(campaign.startDate).toLocaleDateString()} - {new Date(campaign.endDate).toLocaleDateString()}
                    </span>
                  </div>
                </div>
              ))}
            </div>
          </div>
        )}

        <Link 
          href={`/business/${business.businessId}`}
          className="block w-full text-center bg-gradient-to-r from-indigo-600 to-purple-600 text-white px-6 py-3 rounded-xl font-semibold hover:shadow-lg transition-all duration-300 hover:scale-[1.02] group-hover:from-indigo-500 group-hover:to-purple-500"
        >
          Detayları Görüntüle
        </Link>
      </div>
    </div>
  );
} 