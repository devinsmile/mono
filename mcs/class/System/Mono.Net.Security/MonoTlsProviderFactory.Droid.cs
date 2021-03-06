// Copyright 2015 Xamarin Inc. All rights reserved.
#if SECURITY_DEP
using System;
using MSI = Mono.Security.Interface;
#if HAVE_BTLS
using Mono.Btls;
#endif

namespace Mono.Net.Security
{
	static partial class MonoTlsProviderFactory
	{
		static MSI.MonoTlsProvider CreateDefaultProviderImpl ()
		{
			MSI.MonoTlsProvider provider = null;
			var type = Environment.GetEnvironmentVariable ("XA_TLS_PROVIDER");
			switch (type) {
			case null:
			case "default":
			case "legacy":
				return new LegacyTlsProvider ();
			case "btls":
#if HAVE_BTLS
				if (!MonoBtlsProvider.IsSupported ())
					throw new NotSupportedException ("BTLS in not supported!");
				return new MonoBtlsProvider ();
#else
				throw new NotSupportedException ("BTLS in not supported!");
#endif
			default:
				throw new NotSupportedException (string.Format ("Invalid TLS Provider: `{0}'.", provider));
			}
		}
	}
}
#endif
