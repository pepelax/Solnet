﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solnet.Extensions.Models;
using Solnet.Extensions.TokenMint;
using Solnet.Programs;
using Solnet.Rpc;
using Solnet.Rpc.Builders;
using Solnet.Rpc.Core.Http;
using Solnet.Rpc.Messages;
using Solnet.Wallet;
using Solnet.Wallet.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Solnet.Extensions.Test
{

    /// <summary>
    /// Reusing testing mock base class from SolnetRpc.Test project
    /// </summary>
    [TestClass]
    public class TokenWalletTest
    {

        private const string MnemonicWords =
              "route clerk disease box emerge airport loud waste attitude film army tray" +
              " forward deal onion eight catalog surface unit card window walnut wealth medal";

        private const string Blockhash = "5cZja93sopRB9Bkhckj5WzCxCaVyriv2Uh5fFDPDFFfj";


        [TestMethod]
        public void TestLoadKnownMint()
        {
            // get owner
            var ownerWallet = new Wallet.Wallet(MnemonicWords);
            var signer = ownerWallet.GetAccount(1);
            Assert.AreEqual("9we6kjtbcZ2vy3GSLLsZTEhbAqXPTRvEyoxa8wxSqKp5", signer.PublicKey.Key);

            // get mocked RPC client
            var client = new MockTokenWalletRpc();
            client.AddTextFile("Resources/TokenWallet/GetBalanceResponse.json");
            client.AddTextFile("Resources/TokenWallet/GetTokenAccountsByOwnerResponse.json");

            // define some mints
            var tokens = new TokenMintResolver();
            var testToken = new TokenMint.TokenDef("98mCaWvZYTmTHmimisaAQW4WGLphN1cWhcC7KtnZF819", "TEST", "TEST", 2);
            tokens.Add(testToken);
            Assert.AreEqual(125U, testToken.ConvertDecimalToUlong(1.25M));
            Assert.AreEqual(1.25M, testToken.ConvertUlongToDecimal(125U));

            // load account
            var publicKey = "9we6kjtbcZ2vy3GSLLsZTEhbAqXPTRvEyoxa8wxSqKp5";
            var wallet = TokenWallet.Load(client, tokens, publicKey);

            // check wallet 
            Assert.IsNotNull(wallet);
            Assert.AreEqual(publicKey, wallet.PublicKey);
            Assert.AreEqual((ulong)168855000000, wallet.Lamports);
            Assert.AreEqual(168.855M, wallet.Sol);
            Assert.AreEqual(168.855000000M, wallet.Sol);

            // check accounts
            var accounts = wallet.TokenAccounts();
            Assert.IsNotNull(accounts);

            // locate known test mint account
            var testAccounts = wallet.TokenAccounts().WithMint("98mCaWvZYTmTHmimisaAQW4WGLphN1cWhcC7KtnZF819");
            Assert.AreEqual(1, testAccounts.Count());
            Assert.AreEqual((ulong)2039280, testAccounts.First().Lamports);
            Assert.AreEqual(0, testAccounts.WhichAreAssociatedTokenAccounts().Count());
            Assert.AreEqual(1, wallet.TokenAccounts().WithCustomFilter(x => x.PublicKey.StartsWith("G")).Count());
            Assert.AreEqual(2, wallet.TokenAccounts().WithSymbol("TEST").First().DecimalPlaces);
            Assert.AreEqual(testToken.TokenMint, wallet.TokenAccounts().WithSymbol("TEST").First().TokenMint);
            Assert.AreEqual(testToken.Symbol, wallet.TokenAccounts().WithMint("98mCaWvZYTmTHmimisaAQW4WGLphN1cWhcC7KtnZF819").First().Symbol);
            Assert.AreEqual(10M, wallet.TokenAccounts().WithSymbol("TEST").First().QuantityDecimal);
            Assert.AreEqual("G5SA5eMmbqSFnNZNB2fQV9ipHbh9y9KS65aZkAh9t8zv", wallet.TokenAccounts().WithSymbol("TEST").First().PublicKey);
            Assert.AreEqual("9we6kjtbcZ2vy3GSLLsZTEhbAqXPTRvEyoxa8wxSqKp5", wallet.TokenAccounts().WithSymbol("TEST").First().Owner);

        }


        [TestMethod]
        public void TestLoadUnknownMint()
        {
            // get owner
            var ownerWallet = new Wallet.Wallet(MnemonicWords);
            var signer = ownerWallet.GetAccount(1);
            Assert.AreEqual("9we6kjtbcZ2vy3GSLLsZTEhbAqXPTRvEyoxa8wxSqKp5", signer.PublicKey.Key);

            // get mocked RPC client
            var client = new MockTokenWalletRpc();
            client.AddTextFile("Resources/TokenWallet/GetBalanceResponse.json");
            client.AddTextFile("Resources/TokenWallet/GetTokenAccountsByOwnerResponse.json");

            // load account
            var tokens = new TokenMintResolver();
            var wallet = TokenWallet.Load(client, tokens, "9we6kjtbcZ2vy3GSLLsZTEhbAqXPTRvEyoxa8wxSqKp5");

            // check wallet 
            Assert.IsNotNull(wallet);

            // check accounts
            var accounts = wallet.TokenAccounts();
            Assert.IsNotNull(accounts);

            // locate unknown mint account
            var unknownAccounts = wallet.TokenAccounts().WithMint("88ocFjrLgHEMQRMwozC7NnDBQUsq2UoQaqREFZoDEex");
            Assert.AreEqual(1, unknownAccounts.Count());
            Assert.AreEqual(0, unknownAccounts.WhichAreAssociatedTokenAccounts().Count());
            Assert.AreEqual(2, wallet.TokenAccounts().WithMint("88ocFjrLgHEMQRMwozC7NnDBQUsq2UoQaqREFZoDEex").First().DecimalPlaces);
            Assert.AreEqual(10M, wallet.TokenAccounts().WithMint("88ocFjrLgHEMQRMwozC7NnDBQUsq2UoQaqREFZoDEex").First().QuantityDecimal);
            Assert.AreEqual("4NSREK36nAr32vooa3L9z8tu6JWj5rY3k4KnsqTgynvm", wallet.TokenAccounts().WithMint("88ocFjrLgHEMQRMwozC7NnDBQUsq2UoQaqREFZoDEex").First().PublicKey);
            Assert.AreEqual("9we6kjtbcZ2vy3GSLLsZTEhbAqXPTRvEyoxa8wxSqKp5", wallet.TokenAccounts().WithMint("88ocFjrLgHEMQRMwozC7NnDBQUsq2UoQaqREFZoDEex").First().Owner);

        }


        [TestMethod]
        public void TestProvisionAtaInjectBuilder()
        {
            // get owner
            var ownerWallet = new Wallet.Wallet(MnemonicWords);
            var signer = ownerWallet.GetAccount(1);
            Assert.AreEqual("9we6kjtbcZ2vy3GSLLsZTEhbAqXPTRvEyoxa8wxSqKp5", signer.PublicKey.Key);

            // get mocked RPC client
            var client = new MockTokenWalletRpc();
            client.AddTextFile("Resources/TokenWallet/GetBalanceResponse.json");
            client.AddTextFile("Resources/TokenWallet/GetTokenAccountsByOwnerResponse.json");

            // define some mints
            var tokens = new TokenMintResolver();
            var testToken = new TokenMint.TokenDef("98mCaWvZYTmTHmimisaAQW4WGLphN1cWhcC7KtnZF819", "TEST", "TEST", 2);
            tokens.Add(testToken);

            // load account
            var wallet = TokenWallet.Load(client, tokens, "9we6kjtbcZ2vy3GSLLsZTEhbAqXPTRvEyoxa8wxSqKp5");

            // check wallet 
            Assert.IsNotNull(wallet);

            // check accounts
            var accounts = wallet.TokenAccounts();
            Assert.IsNotNull(accounts);

            // locate known test mint account
            var testAccounts = wallet.TokenAccounts().WithMint("98mCaWvZYTmTHmimisaAQW4WGLphN1cWhcC7KtnZF819");
            Assert.AreEqual(1, testAccounts.Count());
            Assert.AreEqual(0, testAccounts.WhichAreAssociatedTokenAccounts().Count());

            // provision the ata
            var builder = new TransactionBuilder();
            builder
                .SetFeePayer(signer)
                .SetRecentBlockHash(Blockhash);
            var before = builder.Build(signer);

            // jit should append create ata command to builder
            var testAta = wallet.JitCreateAssociatedTokenAccount(builder, testToken.TokenMint, new PublicKey("9we6kjtbcZ2vy3GSLLsZTEhbAqXPTRvEyoxa8wxSqKp5"));
            var after = builder.Build(signer);
            Assert.AreEqual("F6qCC87R5cmAJUKbhwERSFQHkQpSKyUkETgrjTJKB2nK", testAta.Key);
            Assert.IsTrue(after.Length > before.Length);

        }


        [TestMethod]
        public void TestLoadRefresh()
        {
            // get owner
            var ownerWallet = new Wallet.Wallet(MnemonicWords);
            var signer = ownerWallet.GetAccount(1);
            Assert.AreEqual("9we6kjtbcZ2vy3GSLLsZTEhbAqXPTRvEyoxa8wxSqKp5", signer.PublicKey.Key);

            // get mocked RPC client
            var client = new MockTokenWalletRpc();
            client.AddTextFile("Resources/TokenWallet/GetBalanceResponse.json");
            client.AddTextFile("Resources/TokenWallet/GetTokenAccountsByOwnerResponse.json");
            client.AddTextFile("Resources/TokenWallet/GetBalanceResponse.json");
            client.AddTextFile("Resources/TokenWallet/GetTokenAccountsByOwnerResponse.json");

            // define some mints
            var tokens = new TokenMintResolver();
            var testToken = new TokenMint.TokenDef("98mCaWvZYTmTHmimisaAQW4WGLphN1cWhcC7KtnZF819", "TEST", "TEST", 2);
            tokens.Add(testToken);

            // load account
            var wallet = TokenWallet.Load(client, tokens, "9we6kjtbcZ2vy3GSLLsZTEhbAqXPTRvEyoxa8wxSqKp5");

            // check wallet 
            Assert.IsNotNull(wallet);

            // refresh
            wallet.Refresh();

        }


        [TestMethod]
        public void TestSendTokenProvisionAta()
        {

            // get owner
            var ownerWallet = new Wallet.Wallet(MnemonicWords);
            var signer = ownerWallet.GetAccount(1);
            Assert.AreEqual("9we6kjtbcZ2vy3GSLLsZTEhbAqXPTRvEyoxa8wxSqKp5", signer.PublicKey.Key);

            // use other account as mock target and check derived PDA
            var mintPubkey = new PublicKey("98mCaWvZYTmTHmimisaAQW4WGLphN1cWhcC7KtnZF819");
            var targetOwner = ownerWallet.GetAccount(99);
            var deterministicPda = AssociatedTokenAccountProgram.DeriveAssociatedTokenAccount(targetOwner, mintPubkey);
            Assert.AreEqual("3FmSwkHqwRdqYQ74Nx84LNYLnwPhcNivuqhDGWghZY7F", targetOwner.PublicKey.Key);
            Assert.IsNotNull(deterministicPda);
            Assert.AreEqual("HwkThm2LadHWCnqaSkJCpQutvrt8qwp2PpSxBHbhcwYV", deterministicPda.Key);

            // get mocked RPC client
            var client = new MockTokenWalletRpc();
            client.AddTextFile("Resources/TokenWallet/GetBalanceResponse.json");
            client.AddTextFile("Resources/TokenWallet/GetTokenAccountsByOwnerResponse.json");

            // define some mints
            var tokens = new TokenMintResolver();
            var testToken = new TokenMint.TokenDef(mintPubkey.Key, "TEST", "TEST", 2);
            tokens.Add(testToken);

            // load account 
            var wallet = TokenWallet.Load(client, tokens, "9we6kjtbcZ2vy3GSLLsZTEhbAqXPTRvEyoxa8wxSqKp5");
            Assert.IsNotNull(wallet);

            // identify test token account with some balance
            var testTokenAccount = wallet.TokenAccounts().ForToken(testToken).WithAtLeast(5M).First();
            Assert.IsFalse(testTokenAccount.IsAssociatedTokenAccount);

            // going to send some TEST token to destination wallet that does not have a
            // an Associated Token Account for that mint (so one will be provisioned)
            // internally, this will trigger a wallet load so need to load two more mock responses
            client.AddTextFile("Resources/TokenWallet/GetBalanceResponse.json");
            client.AddTextFile("Resources/TokenWallet/GetTokenAccountsByOwnerResponse2.json");
            client.AddTextFile("Resources/TokenWallet/GetRecentBlockhashResponse.json");
            client.AddTextFile("Resources/TokenWallet/SendTransactionResponse.json");
            wallet.Send(testTokenAccount, 1M, targetOwner, signer.PublicKey, builder => builder.Build(signer));

        }


        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void TestTokenWalletLoadAddressCheck()
        {
            // try to load a made up wallet address
            var client = new MockTokenWalletRpc();
            var tokens = new TokenMintResolver();
            TokenWallet.Load(client, tokens, "FAKEkjtbcZ2vy3GSLLsZTEhbAqXPTRvEyoxa8wxSqKp5");
        }


        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void TestTokenWalletSendAddressCheck()
        {

            // get owner
            var ownerWallet = new Wallet.Wallet(MnemonicWords);
            var signer = ownerWallet.GetAccount(1);
            Assert.AreEqual("9we6kjtbcZ2vy3GSLLsZTEhbAqXPTRvEyoxa8wxSqKp5", signer.PublicKey.Key);

            // get mocked RPC client
            var client = new MockTokenWalletRpc();
            client.AddTextFile("Resources/TokenWallet/GetBalanceResponse.json");
            client.AddTextFile("Resources/TokenWallet/GetTokenAccountsByOwnerResponse.json");

            // define some mints
            var tokens = new TokenMintResolver();
            var testToken = new TokenMint.TokenDef("98mCaWvZYTmTHmimisaAQW4WGLphN1cWhcC7KtnZF819", "TEST", "TEST", 2);
            tokens.Add(testToken);

            // load account and identify test token account with some balance
            var wallet = TokenWallet.Load(client, tokens, "9we6kjtbcZ2vy3GSLLsZTEhbAqXPTRvEyoxa8wxSqKp5");
            Assert.IsNotNull(wallet);
            var testTokenAccount = wallet.TokenAccounts().ForToken(testToken).WithAtLeast(5M).First();
            Assert.IsFalse(testTokenAccount.IsAssociatedTokenAccount);

            // trigger send to bogus target wallet
            var targetOwner = "BADxzxtbcZ2vy3GSLLsZTEhbAqXPTRvEyoxa8wxSqKp5";
            wallet.Send(testTokenAccount, 1M, targetOwner, signer.PublicKey, builder => builder.Build(signer));

        }


        /// <summary>
        /// Check to make sure callee can not send source TokenWalletAccount from Wallet A using Wallet B
        /// </summary>
        [TestMethod, ExpectedException(typeof(AggregateException))]
        public void TestSendTokenDefendAgainstAccountMismatch()
        {

            // define mints and get owner 
            var client = new MockTokenWalletRpc();
            var mintPubkey = new PublicKey("98mCaWvZYTmTHmimisaAQW4WGLphN1cWhcC7KtnZF819");
            var tokens = new TokenMintResolver();
            var testToken = new TokenMint.TokenDef(mintPubkey.Key, "TEST", "TEST", 2);
            tokens.Add(testToken);
            var ownerWallet = new Wallet.Wallet(MnemonicWords);

            // load wallet a
            var account_a = ownerWallet.GetAccount(1);
            Assert.AreEqual("9we6kjtbcZ2vy3GSLLsZTEhbAqXPTRvEyoxa8wxSqKp5", account_a.PublicKey.Key);
            client.AddTextFile("Resources/TokenWallet/GetBalanceResponse.json");
            client.AddTextFile("Resources/TokenWallet/GetTokenAccountsByOwnerResponse.json");
            var wallet_a = TokenWallet.Load(client, tokens, account_a);

            // load wallet b
            var account_b = ownerWallet.GetAccount(2);
            Assert.AreEqual("3F2RNf2f2kWYgJ2XsqcjzVeh3rsEQnwf6cawtBiJGyKV", account_b.PublicKey.Key);
            client.AddTextFile("Resources/TokenWallet/GetBalanceResponse.json");
            client.AddTextFile("Resources/TokenWallet/GetTokenAccountsByOwnerResponse2.json");
            var wallet_b = TokenWallet.Load(client, tokens, account_b);

            // use other account as mock target and check derived PDA
            var destination = ownerWallet.GetAccount(99);

            // identify test token account with some balance in Wallet A
            var account_in_a = wallet_a.TokenAccounts().ForToken(testToken).WithAtLeast(5M).First();
            Assert.IsFalse(account_in_a.IsAssociatedTokenAccount);

            // attempt to send using wallet b - this should not succeed
            wallet_b.Send(account_in_a, 1M, destination, account_a.PublicKey, builder => builder.Build(account_b));

        }


        [TestMethod]
        public void TestMockJsonRpcParseResponseValue()
        {
            var serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Converters =
                {
                    new TransactionMetaInfoConverter(),
                    new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
                }
            };
            var json = File.ReadAllText("Resources/TokenWallet/GetBalanceResponse.json");
            var result = JsonSerializer.Deserialize<JsonRpcResponse<ResponseValue<ulong>>>(json, serializerOptions);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestMockJsonRpcSendTxParse()
        {
            var serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Converters =
                {
                    new TransactionMetaInfoConverter(),
                    new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
                }
            };
            var json = File.ReadAllText("Resources/TokenWallet/SendTransactionResponse.json");
            var result = JsonSerializer.Deserialize<JsonRpcResponse<string>>(json, serializerOptions);
            Assert.IsNotNull(result);
        }


        [TestMethod]
        public void TestOnCurveSanityChecks()
        {
            // check real wallet address
            var ownerWallet = new Wallet.Wallet(MnemonicWords);
            var owner = ownerWallet.GetAccount(1);
            Assert.AreEqual("9we6kjtbcZ2vy3GSLLsZTEhbAqXPTRvEyoxa8wxSqKp5", owner.PublicKey.Key);
            Assert.IsTrue(owner.PublicKey.IsOnCurve());

            // spot an ata
            var mintPubkey = new PublicKey(WellKnownTokens.Serum.TokenMint);
            var ata = AssociatedTokenAccountProgram.DeriveAssociatedTokenAccount(owner, mintPubkey);
            Assert.IsFalse(ata.IsOnCurve());

            // spot a fake address
            var fake = new PublicKey("FAKEkjtbcZ2vy3GSLLsZTEhbAqXPTRvEyoxa8wxSqKp5");
            Assert.IsFalse(fake.IsOnCurve());

        }

        [TestMethod]
        public void TestTokenWalletViaBatch()
        {

            var expected_request = File.ReadAllText("Resources/TokenWallet/SampleBatchRequest.json");
            var expected_response = File.ReadAllText("Resources/TokenWallet/SampleBatchResponse.json");

            // token resolver
            var tokens = new TokenMintResolver();
            var testToken = new TokenMint.TokenDef("98mCaWvZYTmTHmimisaAQW4WGLphN1cWhcC7KtnZF819", "TEST", "TEST", 2);
            tokens.Add(testToken);

            // init batch for mockery
            var unusedRpcClient = ClientFactory.GetClient(Cluster.MainNet);
            var batch = new SolanaRpcBatchWithCallbacks(unusedRpcClient);

            // test wallet
            var ownerWallet = new Wallet.Wallet(MnemonicWords);
            var signer = ownerWallet.GetAccount(1);
            var pubkey = signer.PublicKey.Key;
            Assert.AreEqual("9we6kjtbcZ2vy3GSLLsZTEhbAqXPTRvEyoxa8wxSqKp5", pubkey);
            var walletPromise = TokenWallet.LoadAsync(batch, tokens, pubkey);

            // serialize batch and check we're good
            var reqs = batch.Composer.CreateJsonRequests();
            var serializerOptions = CreateJsonOptions();
            var json = JsonSerializer.Serialize<JsonRpcBatchRequest>(reqs, serializerOptions);
            Assert.IsNotNull(reqs);
            Assert.AreEqual(2, reqs.Count);
            Assert.AreEqual(expected_request, json);

            // fake RPC response
            var resp = CreateMockRequestResult<JsonRpcBatchResponse>(expected_request, expected_response, HttpStatusCode.OK);
            Assert.IsNotNull(resp.Result);
            Assert.AreEqual(2, resp.Result.Count);

            // process and invoke callbacks - this will unblock walletPromise
            batch.Composer.ProcessBatchResponse(resp);

            // assert wallet
            var wallet = walletPromise.Result;
            Assert.AreEqual((ulong)168855000000, wallet.Lamports);
            Assert.AreEqual(168.855M, wallet.Sol);
            Assert.AreEqual(168.855000000M, wallet.Sol);

            // define some mints
            Assert.AreEqual(10M, wallet.TokenAccounts().WithSymbol("TEST").First().QuantityDecimal);
            Assert.AreEqual(10M, wallet.TokenAccounts().WithMint(testToken).First().QuantityDecimal);
            Assert.AreEqual(10M, wallet.TokenAccounts().WithAtLeast(10M).First().QuantityDecimal);
            Assert.AreEqual(10M, wallet.TokenAccounts().WithAtLeast(1000U).First().QuantityDecimal);
            Assert.AreEqual(10M, wallet.TokenAccounts().WithNonZero().First().QuantityDecimal);

        }

        [TestMethod]
        public void TestTokenWalletFilterList()
        {
            var accounts = new List<TokenWalletAccount>();
            var list = new TokenWalletFilterList(accounts);
            var pass = false;
            try
            {
                list.WithPublicKey((string)null);
            }
            catch (ArgumentException)
            {
                pass = true;
            }
            try
            {
                list.WithMint((TokenDef)null);
            }
            catch (ArgumentNullException)
            {
                pass = pass && true;
            }
            try
            {
                list.WithCustomFilter(null);
            }
            catch (ArgumentNullException)
            {
                pass = pass && true;
            }
            var count = 0;
            foreach (var check in list)
                count++;
            Assert.AreEqual(0, count);
            Assert.IsTrue(pass);
        }

        /// <summary>
        /// Common JSON options
        /// </summary>
        /// <returns></returns>
        private JsonSerializerOptions CreateJsonOptions()
        {
            return new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Converters =
                {
                    new TransactionMetaInfoConverter(),
                    new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
                }
            };
        }

        /// <summary>
        /// Create a mocked RequestResult
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="req"></param>
        /// <param name="resp"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public RequestResult<T> CreateMockRequestResult<T>(string req, string resp, HttpStatusCode status)
        {
            var x = new RequestResult<T>();
            x.HttpStatusCode = status;
            x.RawRpcRequest = req;
            x.RawRpcResponse = resp;

            // deserialize resp
            if (status == HttpStatusCode.OK)
            {
                var serializerOptions = CreateJsonOptions();
                x.Result = JsonSerializer.Deserialize<T>(resp, serializerOptions);
            }

            return x;
        }

    }



}
