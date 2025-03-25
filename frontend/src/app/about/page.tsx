'use client';

import React from 'react';
import { FaHandshake, FaChartLine, FaUsers, FaLightbulb, FaHeart } from 'react-icons/fa';

export default function AboutPage() {
  return (
    <div className="min-h-screen bg-gray-900">
      {/* Hero Section */}
      <div className="relative bg-gradient-to-br from-gray-900 via-indigo-900 to-purple-900 py-24">
        <div className="absolute inset-0 bg-[url('/grid.svg')] bg-center [mask-image:linear-gradient(180deg,white,rgba(255,255,255,0))]" />
        <div className="container mx-auto px-4 relative">
          <h1 className="text-5xl md:text-7xl font-bold text-white mb-8 font-display bg-clip-text text-transparent bg-gradient-to-r from-indigo-400 to-purple-400">
            Hakkımızda
          </h1>
          <p className="text-xl text-gray-300 max-w-2xl">
            Yerel ekonomiyi desteklemek ve işletmeleri bir araya getirmek için çalışıyoruz.
          </p>
        </div>
      </div>

      {/* Misyon ve Vizyon */}
      <div className="container mx-auto px-4 py-16">
        <div className="grid grid-cols-1 md:grid-cols-2 gap-12">
          <div className="bg-gray-800/80 backdrop-blur-lg rounded-2xl p-8 shadow-xl border border-gray-700/50 hover:border-indigo-500/50 transition-all duration-300">
            <div className="w-16 h-16 bg-indigo-900/50 rounded-xl flex items-center justify-center mb-6">
              <FaHandshake className="w-8 h-8 text-indigo-400" />
            </div>
            <h2 className="text-2xl font-bold text-white mb-4">Misyonumuz</h2>
            <p className="text-gray-300">
              Yerel işletmeleri destekleyerek, ekonomik kalkınmaya katkıda bulunmak ve toplumsal refahı artırmak için çalışıyoruz. 
              İşletmelerin dijital dünyada daha görünür olmasını sağlayarak, müşterilerle buluşmalarına yardımcı oluyoruz.
            </p>
          </div>

          <div className="bg-gray-800/80 backdrop-blur-lg rounded-2xl p-8 shadow-xl border border-gray-700/50 hover:border-purple-500/50 transition-all duration-300">
            <div className="w-16 h-16 bg-purple-900/50 rounded-xl flex items-center justify-center mb-6">
              <FaChartLine className="w-8 h-8 text-purple-400" />
            </div>
            <h2 className="text-2xl font-bold text-white mb-4">Vizyonumuz</h2>
            <p className="text-gray-300">
              Türkiye'nin her yerinde yerel işletmelerin dijital dönüşümüne öncülük ederek, 
              sürdürülebilir bir ekonomik büyüme modeli oluşturmak ve toplumsal kalkınmaya katkıda bulunmak.
            </p>
          </div>
        </div>
      </div>

      {/* Değerlerimiz */}
      <div className="container mx-auto px-4 py-16">
        <h2 className="text-3xl font-bold text-white mb-12 text-center">Değerlerimiz</h2>
        <div className="grid grid-cols-1 md:grid-cols-3 gap-8">
          <div className="bg-gray-800/80 backdrop-blur-lg rounded-2xl p-6 shadow-xl border border-gray-700/50 hover:border-green-500/50 transition-all duration-300">
            <div className="w-12 h-12 bg-green-900/50 rounded-xl flex items-center justify-center mb-4">
              <FaUsers className="w-6 h-6 text-green-400" />
            </div>
            <h3 className="text-xl font-semibold text-white mb-2">Topluluk</h3>
            <p className="text-gray-300">
              Yerel işletmeler ve müşteriler arasında güçlü bir topluluk oluşturarak, 
              karşılıklı fayda sağlayan bir ekosistem kuruyoruz.
            </p>
          </div>

          <div className="bg-gray-800/80 backdrop-blur-lg rounded-2xl p-6 shadow-xl border border-gray-700/50 hover:border-yellow-500/50 transition-all duration-300">
            <div className="w-12 h-12 bg-yellow-900/50 rounded-xl flex items-center justify-center mb-4">
              <FaLightbulb className="w-6 h-6 text-yellow-400" />
            </div>
            <h3 className="text-xl font-semibold text-white mb-2">Yenilikçilik</h3>
            <p className="text-gray-300">
              Sürekli gelişen teknoloji ve değişen ihtiyaçlara uyum sağlayarak, 
              yenilikçi çözümler sunuyoruz.
            </p>
          </div>

          <div className="bg-gray-800/80 backdrop-blur-lg rounded-2xl p-6 shadow-xl border border-gray-700/50 hover:border-red-500/50 transition-all duration-300">
            <div className="w-12 h-12 bg-red-900/50 rounded-xl flex items-center justify-center mb-4">
              <FaHeart className="w-6 h-6 text-red-400" />
            </div>
            <h3 className="text-xl font-semibold text-white mb-2">Sürdürülebilirlik</h3>
            <p className="text-gray-300">
              Çevreye ve topluma duyarlı, uzun vadeli ve sürdürülebilir bir 
              ekonomik model oluşturmayı hedefliyoruz.
            </p>
          </div>
        </div>
      </div>

      {/* İletişim */}
      <div className="container mx-auto px-4 py-16">
        <div className="bg-gray-800/80 backdrop-blur-lg rounded-2xl p-8 shadow-xl border border-gray-700/50">
          <h2 className="text-3xl font-bold text-white mb-6 text-center">Bizimle İletişime Geçin</h2>
          <p className="text-gray-300 text-center max-w-2xl mx-auto mb-8">
            Sorularınız, önerileriniz veya işbirliği talepleriniz için bizimle iletişime geçebilirsiniz.
          </p>
          <div className="flex justify-center">
            <a
              href="mailto:info@yerelekonomi.com"
              className="px-8 py-4 bg-indigo-600 text-white rounded-xl font-semibold hover:bg-indigo-700 transition-colors"
            >
              E-posta Gönder
            </a>
          </div>
        </div>
      </div>
    </div>
  );
} 