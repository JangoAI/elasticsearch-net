﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// The delete by query API allows to delete documents from one or more indices and one or more types based on a query.
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-delete-by-query.html
		/// </summary>
		/// <typeparam name="T">The type used to infer the default index and typename as well as describe the strongly
		///  typed parts of the query</typeparam>
		/// <param name="deleteByQuerySelector">An optional descriptor to further describe the delete by query operation</param>
		IDeleteByQueryResponse DeleteByQuery<T>(Func<DeleteByQueryDescriptor<T>, IDeleteByQueryRequest> deleteByQuerySelector)
			where T : class;

		/// <inheritdoc/>
		IDeleteByQueryResponse DeleteByQuery(IDeleteByQueryRequest deleteByQueryRequest);

		/// <inheritdoc/>
		Task<IDeleteByQueryResponse> DeleteByQueryAsync<T>(Func<DeleteByQueryDescriptor<T>, IDeleteByQueryRequest> deleteByQuerySelector)
			where T : class;

		/// <inheritdoc/>
		Task<IDeleteByQueryResponse> DeleteByQueryAsync(IDeleteByQueryRequest deleteByQueryRequest);

	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IDeleteByQueryResponse DeleteByQuery<T>(Func<DeleteByQueryDescriptor<T>, IDeleteByQueryRequest> deleteByQuerySelector) where T : class =>
			this.DeleteByQuery(deleteByQuerySelector?.Invoke(new DeleteByQueryDescriptor<T>()));

		/// <inheritdoc/>
		public IDeleteByQueryResponse DeleteByQuery(IDeleteByQueryRequest deleteByQueryRequest) => 
			this.Dispatcher.Dispatch<IDeleteByQueryRequest, DeleteByQueryRequestParameters, DeleteByQueryResponse>(
				deleteByQueryRequest,
				this.LowLevelDispatch.DeleteByQueryDispatch<DeleteByQueryResponse>
			);

		/// <inheritdoc/>
		public Task<IDeleteByQueryResponse> DeleteByQueryAsync<T>(Func<DeleteByQueryDescriptor<T>, IDeleteByQueryRequest> deleteByQuerySelector) where T : class =>
			this.DeleteByQueryAsync(deleteByQuerySelector?.Invoke(new DeleteByQueryDescriptor<T>()));

		/// <inheritdoc/>
		public Task<IDeleteByQueryResponse> DeleteByQueryAsync(IDeleteByQueryRequest deleteByQueryRequest) => 
			this.Dispatcher.DispatchAsync<IDeleteByQueryRequest, DeleteByQueryRequestParameters, DeleteByQueryResponse, IDeleteByQueryResponse>(
				deleteByQueryRequest,
				this.LowLevelDispatch.DeleteByQueryDispatchAsync<DeleteByQueryResponse>
			);
	}
}