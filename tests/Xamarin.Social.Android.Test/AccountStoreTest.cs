using System;
using NUnit.Framework;
using Android.NUnit;
using Xamarin.Social.Services;
using System.Linq;
using Xamarin.Auth;

namespace Xamarin.Social.Android.Test
{
	[TestFixture]
	public class AccountManagerStoreTest
	{
		[Test]
		public void SaveNew ()
		{
			var rand = new Random ();
			var s = AccountStore.Create (TestRunner.Shared);
			s.Save (new Account (rand.Next ().ToString ()), "Test");
		}

		[Test]
		public void Update ()
		{
			var rand = new Random ();
			var s = AccountStore.Create (TestRunner.Shared);

			var acct = new Account (rand.Next ().ToString ());
			s.Save (acct, "Test");

			var storedAcct = s.FindAccountsForService ("Test").FirstOrDefault (x => x.Username == acct.Username);
			Assert.That (storedAcct, Is.Not.Null ());

			storedAcct.Properties["foo"] = "bar";

			s.Save (storedAcct, "Test");

			var updatedAcct = s.FindAccountsForService ("Test").FirstOrDefault (x => x.Username == acct.Username);
			Assert.That (updatedAcct, Is.Not.Null ());

			Assert.That ("bar", Is.EqualTo (updatedAcct.Properties["foo"]));
		}
	}
}
