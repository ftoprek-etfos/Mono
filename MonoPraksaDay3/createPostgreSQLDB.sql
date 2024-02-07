CREATE TABLE "CashRegister" (
  "Id" SERIAL PRIMARY KEY,
  "AvailableFunds" decimal(10,2) NOT NULL
);

CREATE TABLE "Worker" (
  "Id" SERIAL PRIMARY KEY,
  "FirstName" varchar(50) NOT NULL,
  "LastName" varchar(50) NOT NULL,
  "RegisterId" int NOT NULL,
   CONSTRAINT "FK_Worker_CashRegister_Id" FOREIGN KEY ("RegisterId") REFERENCES "CashRegister" ("Id")
);

CREATE TABLE "ProductCategory" (
  "Id" SERIAL PRIMARY KEY,
  "CategoryName" varchar(50) NOT NULL
);

CREATE TABLE "Product" (
  "Id" SERIAL PRIMARY KEY,
  "Name" varchar(100) NOT NULL,
  "Price" decimal(8,2) NOT NULL,
  "Stock" int NOT NULL,
  "ProductCategoryId" int NOT NULL,
   CONSTRAINT "FK_Product_ProductCategory_Id" FOREIGN KEY ("ProductCategoryId") REFERENCES "ProductCategory" ("Id")
);

CREATE TABLE "PaymentMethod" (
  "Id" SERIAL PRIMARY KEY,
  "PaymentName" varchar(10) NOT NULL
);

CREATE TABLE "Receipt" (
  "Id" SERIAL PRIMARY KEY,
  "RegisterId" int NOT NULL,
  "DateOfPurchase" timestamp DEFAULT (now()),
  "TransactionStatus" varchar(10) NOT NULL,
  "PaymentMethod" int NOT NULL,
   CONSTRAINT "FK_Receipt_Product_Id" FOREIGN KEY ("RegisterId") REFERENCES "CashRegister" ("Id"),
   CONSTRAINT "FK_Receipt_PaymentMethod_Id" FOREIGN KEY ("PaymentMethod") REFERENCES "PaymentMethod" ("Id")
);

CREATE TABLE "OrderInfo" (
  "Id" SERIAL PRIMARY KEY,
  "ProductId" int NOT NULL,
  "ReceiptId" int NOT NULL,
  "Quantity" int NOT NULL,
  "Total" decimal(8,2) NOT NULL,
   CONSTRAINT "FK_OrderInfo_Product_Id" FOREIGN KEY ("ProductId") REFERENCES "Product" ("Id"),
   CONSTRAINT "FK_OrderInfo_Receipt_Id" FOREIGN KEY ("ReceiptId") REFERENCES "Receipt" ("Id")
);

CREATE INDEX "ProductCategoryAndStockIndex" ON "Product" ("ProductCategoryId", "Stock");

CREATE INDEX "DateOfPurchaseIndex" ON "Receipt" ("DateOfPurchase");