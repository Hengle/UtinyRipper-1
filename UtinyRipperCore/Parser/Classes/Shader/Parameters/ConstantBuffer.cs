﻿using System.Collections.Generic;

namespace UtinyRipper.Classes.Shaders
{
	public struct ConstantBuffer : IAssetReadable
	{
		/// <summary>
		/// 2017.3 and greater
		/// </summary>
		public static bool IsReadStructParams(Version version)
		{
			return version.IsGreaterEqual(2017, 3);
		}

		public ConstantBuffer(string name, MatrixParameter[] matrices, VectorParameter[] vectors, int usedSize)
		{
			Name = name;
			NameIndex = -1;
			m_matrixParams = matrices;
			m_vectorParams = vectors;
			m_structParams = null;
			Size = usedSize;
		}

		public void Read(AssetStream stream)
		{
			NameIndex = stream.ReadInt32();
			m_matrixParams = stream.ReadArray<MatrixParameter>();
			m_vectorParams = stream.ReadArray<VectorParameter>();
			if(IsReadStructParams(stream.Version))
			{
				m_structParams = stream.ReadArray<StructParameter>();
			}
			Size = stream.ReadInt32();
		}

		public string Name { get; private set; }
		public int NameIndex { get; private set; }
		public IReadOnlyList<MatrixParameter> MatrixParams => m_matrixParams;
		public IReadOnlyList<VectorParameter> VectorParams => m_vectorParams;
		public IReadOnlyList<StructParameter> StructParams => m_structParams;
		public int Size { get; private set; }
		
		private MatrixParameter[] m_matrixParams;
		private VectorParameter[] m_vectorParams;
		private StructParameter[] m_structParams;
	}
}
