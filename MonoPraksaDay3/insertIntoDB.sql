INSERT INTO "CashRegister" ("AvailableFunds") VALUES (2550);
INSERT INTO "CashRegister" ("AvailableFunds") VALUES (3588);

UPDATE "CashRegister" SET "AvailableFunds" = 3555 WHERE ("AvailableFunds" = 3588);

INSERT INTO "Worker" ("FirstName","LastName","RegisterId") VALUES ('Filip','Toprek',2);
INSERT INTO "Worker" ("FirstName","LastName","RegisterId") VALUES ('Marko','Maric',3);

INSERT INTO "ProductCategory" ("CategoryName") VALUES ('Food'),('Garden');
INSERT INTO "ProductCategory" ("CategoryName") VALUES ('Home Appliances'), ('Sports Equipment'), ('Books'), ('Health and Beauty'), ('Stationery');

INSERT INTO "Product" ("Name","Price","Stock","ProductCategoryId") VALUES ('Lawn mower',700,5,2),('Bagel',1,25,1);
INSERT INTO "Product" ("Name", "Price", "Stock", "ProductCategoryId") VALUES ('Refrigerator', 1200, 8, 3), ('Tennis Racket', 50, 15, 4), ('Novel - The Great Adventure', 15, 30, 5), ('Shampoo', 8, 40, 6), ('Notebook Set', 10, 20, 7);

INSERT INTO "PaymentMethod" ("PaymentName") VALUES ('Cash'),('Card');

INSERT INTO "Receipt" ("RegisterId","TransactionStatus","PaymentMethod") VALUES (2,'Success',1);
INSERT INTO "Receipt" ("RegisterId", "TransactionStatus", "PaymentMethod") VALUES (3, 'Success', 2), (2, 'Pending', 2), (3, 'Failed', 2), (2, 'Success', 1), (3, 'Success', 1);

INSERT INTO "OrderInfo" ("ProductId","ReceiptId","Quantity","Total") VALUES (1,2,1,700.00);
INSERT INTO "OrderInfo" ("ProductId", "ReceiptId", "Quantity", "Total") VALUES (3, 3, 2, 2400.00), (5, 4, 3, 45.00), (6, 5, 4, 32.00), (6, 6, 2, 16.00), (4, 7, 1, 50.00);

SELECT * FROM "Receipt" INNER JOIN "OrderInfo" ON "Receipt"."Id" = "OrderInfo"."ReceiptId" WHERE "OrderInfo"."Total" <= 50

SELECT * FROM "Receipt" RIGHT JOIN "OrderInfo" ON "Receipt"."Id" = "OrderInfo"."ReceiptId" JOIN "PaymentMethod" ON "Receipt"."PaymentMethod" = "PaymentMethod"."Id" WHERE "Receipt"."TransactionStatus" LIKE 'Succ%'

SELECT * FROM "Product" LEFT JOIN "OrderInfo" ON "Product"."Id" = "OrderInfo"."ProductId"