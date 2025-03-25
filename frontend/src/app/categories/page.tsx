'use client';

import React, { useEffect, useState } from 'react';
import { Category } from '../../services/api';
import { categoryService } from '../../services/api';
import { FaSearch, FaStore, FaTags, FaArrowRight } from 'react-icons/fa';
import Link from 'next/link';

export default function CategoriesPage() {
  const [categories, setCategories] = useState<Category[]>([]);
  const [loading, setLoading] = useState(true);
  const [searchTerm, setSearchTerm] = useState('');
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchCategories = async () => {
      try {
        const data = await categoryService.getAll();
        setCategories(data);
      } catch (error) {
        setError('Kategoriler yüklenirken bir hata oluştu. Lütfen sayfayı yenileyin.');
      } finally {
        setLoading(false);
      }
    };

    fetchCategories();
  }, []);

  const filteredCategories = categories.filter(category => {
    return !searchTerm || 
      category.name.toLowerCase().includes(searchTerm.toLowerCase());
  });

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

  if (error) {
    return (
      <div className="min-h-screen flex items-center justify-center bg-gray-900">
        <div className="text-center p-12 bg-gray-800/80 backdrop-blur-lg rounded-3xl shadow-2xl max-w-lg mx-4 border border-gray-700">
          <div className="w-20 h-20 mx-auto mb-6 rounded-full bg-red-900/50 flex items-center justify-center">
            <svg className="w-10 h-10 text-red-500" fill="none" stroke="currentColor" viewBox="0 0 24 24">
              <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M12 9v2m0 4h.01m-6.938 4h13.856c1.54 0 2.502-1.667 1.732-3L13.732 4c-.77-1.333-2.694-1.333-3.464 0L3.34 16c-.77 1.333.192 3 1.732 3z" />
            </svg>
          </div>
          <h2 className="text-3xl font-bold text-red-500 mb-4">Hata</h2>
          <p className="text-gray-300 mb-8">{error}</p>
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
          <div className="max-w-4xl mx-auto text-center">
            <h1 className="text-5xl md:text-6xl font-bold text-white mb-8 leading-tight font-display">
              Kategoriler
            </h1>
            <p className="text-xl text-gray-300 mb-12 max-w-2xl mx-auto">
              İşletmeleri kategorilere göre keşfedin
            </p>
            
            {/* Arama Çubuğu */}
            <div className="max-w-2xl mx-auto">
              <div className="relative group">
                <div className="absolute -inset-0.5 bg-gradient-to-r from-indigo-600 to-purple-600 rounded-full blur opacity-50 group-hover:opacity-75 transition duration-1000 group-hover:duration-200" />
                <div className="relative flex items-center bg-gray-800 rounded-full p-1 border border-gray-700">
                  <input
                    type="text"
                    placeholder="Kategori ara..."
                    value={searchTerm}
                    onChange={(e) => setSearchTerm(e.target.value)}
                    className="w-full px-6 py-4 rounded-full text-gray-100 bg-transparent focus:outline-none placeholder-gray-500"
                  />
                  <button className="px-8 py-4 bg-gradient-to-r from-indigo-600 to-purple-600 text-white rounded-full font-semibold hover:shadow-lg transition-all duration-300 hover:scale-105 flex items-center space-x-2">
                    <FaSearch className="w-5 h-5" />
                    <span>Ara</span>
                  </button>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>

      {/* Ana İçerik */}
      <div className="container mx-auto px-4 py-16">
        {/* Sonuç Sayısı */}
        <div className="mb-8">
          <p className="text-lg text-gray-400 font-medium">
            {filteredCategories.length} kategori bulundu
          </p>
        </div>

        {/* Kategori Listesi */}
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-8">
          {filteredCategories.map((category) => (
            <Link
              key={category.categoryId}
              href={`/businesses?category=${category.categoryId}`}
              className="group bg-gray-800/80 backdrop-blur-lg rounded-2xl p-8 shadow-xl hover:shadow-2xl transition-all duration-300 hover:-translate-y-1 border border-gray-700"
            >
              <div className="w-16 h-16 bg-indigo-900/50 rounded-xl flex items-center justify-center mb-6 group-hover:scale-110 transition-transform duration-300">
                <FaTags className="w-8 h-8 text-indigo-400" />
              </div>
              <h3 className="text-2xl font-bold text-white mb-4 font-display group-hover:text-indigo-400 transition-colors duration-300">
                {category.name}
              </h3>
              <div className="flex items-center text-gray-400 group-hover:text-indigo-400 transition-colors duration-300">
                <span>İşletmeleri Görüntüle</span>
                <FaArrowRight className="w-5 h-5 ml-2 group-hover:translate-x-1 transition-transform duration-300" />
              </div>
            </Link>
          ))}
        </div>

        {/* Sonuç Bulunamadı */}
        {filteredCategories.length === 0 && (
          <div className="text-center py-16 bg-gray-800/80 backdrop-blur-lg rounded-3xl shadow-xl border border-gray-700">
            <div className="relative w-24 h-24 mx-auto mb-8">
              <div className="absolute inset-0 border-4 border-indigo-500 border-t-transparent rounded-full animate-spin" />
              <div className="absolute inset-4 border-4 border-purple-500 border-t-transparent rounded-full animate-spin-reverse" />
            </div>
            <h3 className="text-2xl font-bold text-white mb-4 font-display">Sonuç Bulunamadı</h3>
            <p className="text-gray-400 max-w-md mx-auto">
              Aradığınız kriterlere uygun kategori bulunamadı. Lütfen farklı bir arama yapın.
            </p>
          </div>
        )}
      </div>
    </div>
  );
} 