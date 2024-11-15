using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RedisCaching.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "Name", "Price", "Quantity" },
                values: new object[,]
                {
                    { new Guid("00e657e3-6c41-48b4-9611-9ab09c701aae"), "Vegetables", "Product 49", 293, 105 },
                    { new Guid("0161384d-379d-4945-8d46-335752e15a59"), "Beverages", "Product 75", 437, 154 },
                    { new Guid("0e5deb03-7a8c-47e6-9bfc-7808ff134959"), "Fruits", "Product 90", 375, 43 },
                    { new Guid("13b59e9e-791b-4e3f-87d1-d0451320c634"), "Vegetables", "Product 42", 779, 54 },
                    { new Guid("13d2ad0d-e5ed-4b7d-9241-63f8b33ed81d"), "Beverages", "Product 96", 423, 178 },
                    { new Guid("151f6a33-76ea-4b5b-b75d-3d3d6bdf8506"), "Beverages", "Product 59", 151, 94 },
                    { new Guid("16eed478-ec3b-40fb-8ae6-5fedd0fb192c"), "Beverages", "Product 61", 476, 29 },
                    { new Guid("172deefd-612a-43b6-a403-fe5d257e801c"), "Fruits", "Product 15", 801, 28 },
                    { new Guid("17a8f217-7309-4918-ac48-5d666e752c80"), "Beverages", "Product 60", 739, 62 },
                    { new Guid("1a79ffe8-1f38-4632-8feb-224fe35dbb4d"), "Fruits", "Product 11", 613, 71 },
                    { new Guid("1cb0ff98-37d0-426f-8c19-b907cc65b2a4"), "Beverages", "Product 24", 595, 186 },
                    { new Guid("1db9f0c7-69d2-409d-8438-ddb929675e21"), "Beverages", "Product 80", 556, 154 },
                    { new Guid("1dbadd98-695a-483e-906a-37a4241aeaa7"), "Vegetables", "Product 73", 172, 151 },
                    { new Guid("203df8ab-0739-401a-ace5-4ced6718308c"), "Fruits", "Product 70", 732, 38 },
                    { new Guid("229bc634-3981-49cb-ac71-b5bb2baf27c4"), "Vegetables", "Product 9", 207, 173 },
                    { new Guid("239a6181-a70a-4e3d-868d-4eb4eb4a20df"), "Fruits", "Product 32", 833, 97 },
                    { new Guid("28ed9f67-13be-4b96-b66c-3ab73004af83"), "Vegetables", "Product 66", 631, 46 },
                    { new Guid("2a97bb3d-132e-4a64-9f1f-4f2214f0ce51"), "Beverages", "Product 3", 451, 13 },
                    { new Guid("2ebddda5-431d-4471-989b-21da69f22312"), "Vegetables", "Product 23", 138, 74 },
                    { new Guid("2eec67be-d3cf-410f-a888-e4eb5369d517"), "Vegetables", "Product 94", 103, 156 },
                    { new Guid("3380922c-c121-4d47-ac4a-a26ec778442b"), "Fruits", "Product 38", 909, 23 },
                    { new Guid("33eae510-f473-4053-930d-e8aa9516a243"), "Beverages", "Product 55", 226, 147 },
                    { new Guid("343ac576-f5a3-4e59-91d3-9c071768bc8f"), "Vegetables", "Product 88", 29, 110 },
                    { new Guid("38116ba5-9534-418e-8d41-c25a1a64ca4d"), "Beverages", "Product 95", 939, 101 },
                    { new Guid("38795188-04b9-4984-a7d2-c57d4261af0d"), "Beverages", "Product 91", 333, 169 },
                    { new Guid("3c148cd0-7d4f-4149-abb2-78432953be2a"), "Beverages", "Product 69", 593, 70 },
                    { new Guid("3df9fc21-f003-45b7-a131-8de8713d99d0"), "Fruits", "Product 87", 684, 45 },
                    { new Guid("3e0eabd7-4aae-4201-b4ee-9533045fa884"), "Vegetables", "Product 12", 549, 69 },
                    { new Guid("3f9a48db-122f-4c89-8e8d-c49ecae5099f"), "Fruits", "Product 98", 931, 99 },
                    { new Guid("44731ebe-04b4-4a4d-ba2f-7c4431d1a97d"), "Fruits", "Product 77", 440, 34 },
                    { new Guid("4b30ba54-9b82-41be-b782-f90da1772ef0"), "Vegetables", "Product 46", 893, 82 },
                    { new Guid("4f8c333f-80d1-4b63-95e1-a114f3e323fa"), "Fruits", "Product 44", 401, 156 },
                    { new Guid("516ec62e-e31d-4204-a22b-04be510e744f"), "Vegetables", "Product 33", 479, 14 },
                    { new Guid("52357935-1456-4673-9616-06637fe739ec"), "Beverages", "Product 17", 149, 27 },
                    { new Guid("53449cdc-7833-4542-a7df-edf0d6ab5ded"), "Beverages", "Product 19", 687, 141 },
                    { new Guid("55c36314-96db-4ad8-aee7-50a1e7acda48"), "Beverages", "Product 50", 13, 56 },
                    { new Guid("58db086f-274a-411b-b438-dc477be28d37"), "Vegetables", "Product 14", 915, 142 },
                    { new Guid("5ef75012-5315-4f83-be4d-4155ef35f652"), "Fruits", "Product 34", 729, 140 },
                    { new Guid("607dace7-2551-4b9c-859b-2b72f7d73148"), "Vegetables", "Product 62", 980, 100 },
                    { new Guid("63eeebab-0ac4-479e-94af-5e52b593a6b0"), "Vegetables", "Product 27", 443, 16 },
                    { new Guid("65443c06-a7aa-4e8a-848e-d85e43c176e2"), "Beverages", "Product 84", 866, 160 },
                    { new Guid("66d2615d-0ad5-4ec5-b6a5-acd1c935e515"), "Beverages", "Product 97", 42, 185 },
                    { new Guid("6758b683-de7c-4b0a-afbf-b50fad6e2eea"), "Beverages", "Product 78", 844, 23 },
                    { new Guid("691620a7-4444-4f0f-a894-5f4c651a067a"), "Vegetables", "Product 54", 170, 161 },
                    { new Guid("6a19520e-5c86-4ebf-820e-8c0baa6c9fc9"), "Beverages", "Product 89", 993, 20 },
                    { new Guid("6dbe6c77-5fbb-40c7-870d-3bacb14c7b7a"), "Fruits", "Product 35", 396, 32 },
                    { new Guid("6e00ca8e-e38d-4ea9-b9f3-c8c2ffe11b08"), "Fruits", "Product 43", 410, 121 },
                    { new Guid("7165f63d-5350-41d3-9cfa-f5b519d044a1"), "Vegetables", "Product 40", 300, 95 },
                    { new Guid("741e7756-498c-404f-9e69-c2bbb8a5a0a9"), "Fruits", "Product 26", 193, 109 },
                    { new Guid("748dbd26-3d9c-41b2-b436-4f668a0c88c2"), "Beverages", "Product 51", 278, 71 },
                    { new Guid("74a50e98-1162-44a1-a88a-48a228f60412"), "Vegetables", "Product 83", 828, 158 },
                    { new Guid("773130a3-f3bf-431d-b6ce-436e213a564c"), "Fruits", "Product 72", 600, 16 },
                    { new Guid("7926c578-87bd-45dc-a396-2691b21bb80e"), "Fruits", "Product 65", 755, 75 },
                    { new Guid("794dfc67-1c87-4bdf-8395-925f6f6452aa"), "Fruits", "Product 39", 976, 15 },
                    { new Guid("862b2f69-6ab0-4e8b-9785-62fdba7cd06b"), "Beverages", "Product 21", 880, 55 },
                    { new Guid("8d5a17ec-dd1c-47a0-8820-123e126d6021"), "Fruits", "Product 29", 793, 80 },
                    { new Guid("8dcd527e-f6ad-4679-92ba-aef2d167e047"), "Beverages", "Product 52", 744, 187 },
                    { new Guid("8e7b0fac-9286-4437-acf8-3fb30137a11e"), "Fruits", "Product 18", 282, 101 },
                    { new Guid("8f38cee9-e6f4-4f6e-88ef-10dcb7bf8def"), "Vegetables", "Product 7", 878, 120 },
                    { new Guid("8f90f953-7dad-4721-b23f-4e9d4675f139"), "Fruits", "Product 92", 838, 156 },
                    { new Guid("958c8ab9-670e-48d7-8ac5-aa3bef145ca6"), "Vegetables", "Product 22", 871, 174 },
                    { new Guid("9590f2d1-989a-4c84-9ca5-683a1b81820d"), "Vegetables", "Product 93", 533, 110 },
                    { new Guid("9654861f-8dda-4275-bdd2-361add852ab8"), "Fruits", "Product 67", 208, 157 },
                    { new Guid("9de819fd-e6f6-465a-9499-86237ea70c7f"), "Vegetables", "Product 57", 393, 198 },
                    { new Guid("9e02bb8a-e42c-4195-9779-79f15e16c652"), "Vegetables", "Product 58", 483, 84 },
                    { new Guid("9fc50679-302f-4f79-8bcc-ac76ff88844e"), "Vegetables", "Product 68", 595, 192 },
                    { new Guid("a7f96f0f-91e1-4ce2-b923-cea966f411df"), "Vegetables", "Product 82", 544, 148 },
                    { new Guid("aa099b76-e195-462c-a1f2-6597be3b94df"), "Fruits", "Product 1", 750, 174 },
                    { new Guid("aa7d7847-d85e-4f17-b989-a9e461eef093"), "Fruits", "Product 25", 863, 182 },
                    { new Guid("abde2021-26a9-47e6-bcce-c506a53e9227"), "Vegetables", "Product 6", 572, 106 },
                    { new Guid("afb125a3-bd1c-4256-92c7-31395999a828"), "Beverages", "Product 47", 394, 192 },
                    { new Guid("b62d79e3-784d-4682-b754-544225aee246"), "Beverages", "Product 30", 874, 39 },
                    { new Guid("b76ca3c0-33aa-4af5-b5a3-e328c70b84e4"), "Fruits", "Product 48", 261, 100 },
                    { new Guid("b7a2dae3-23b8-49a2-bd81-cd904c905678"), "Vegetables", "Product 71", 972, 169 },
                    { new Guid("bb3f0772-9a01-4c55-9abc-ef6d4f4b7ad9"), "Fruits", "Product 8", 889, 195 },
                    { new Guid("bcc46c17-4ab6-403c-9fe3-840a674d4d70"), "Fruits", "Product 85", 851, 65 },
                    { new Guid("c14a0fb9-2472-4736-a637-c61cbb23a56d"), "Beverages", "Product 28", 173, 57 },
                    { new Guid("c77b8571-31f9-447d-96b8-707096ee5c53"), "Beverages", "Product 81", 111, 53 },
                    { new Guid("c939bb0d-21f0-45df-bb07-189746021d20"), "Beverages", "Product 76", 12, 27 },
                    { new Guid("cc39e406-eaf2-4cda-912d-b88e31927489"), "Beverages", "Product 5", 308, 189 },
                    { new Guid("cf09104b-cd3d-4f55-8715-2e897fde2028"), "Vegetables", "Product 56", 286, 197 },
                    { new Guid("d1dd501e-a1c4-4e65-9a92-9f84a64979c2"), "Beverages", "Product 36", 724, 121 },
                    { new Guid("d300b3e6-6855-4d7a-92ed-eed6e419ede5"), "Beverages", "Product 45", 545, 31 },
                    { new Guid("d4e699d0-f274-43de-841b-0fc639c2c4c2"), "Vegetables", "Product 31", 518, 145 },
                    { new Guid("da29b8a6-67fe-431a-a1b1-e44393a2da62"), "Beverages", "Product 100", 771, 147 },
                    { new Guid("dc9c9a21-31ce-4404-b0c8-fa6ad85d1371"), "Vegetables", "Product 16", 110, 38 },
                    { new Guid("dd799072-26ff-4fac-aab6-9e8eeef17611"), "Vegetables", "Product 41", 10, 80 },
                    { new Guid("de1adc37-1b3c-4c34-bb15-58b4e3f95a47"), "Vegetables", "Product 37", 481, 191 },
                    { new Guid("e03aac4c-0277-4d1f-8e0d-65323064dd8f"), "Vegetables", "Product 53", 845, 180 },
                    { new Guid("eb602a08-cea2-4083-be05-88eff2c536ff"), "Beverages", "Product 64", 631, 54 },
                    { new Guid("ec55632a-9358-43ba-b70e-90abb5c3184e"), "Fruits", "Product 86", 992, 192 },
                    { new Guid("f1a8d97c-8a42-4947-8847-a25611d4a5fd"), "Fruits", "Product 79", 397, 150 },
                    { new Guid("f53876e5-7ab5-48b4-ba5a-35b470318345"), "Fruits", "Product 74", 329, 113 },
                    { new Guid("f63f6707-e955-49cd-8f8d-28f9be5f21ce"), "Beverages", "Product 63", 646, 140 },
                    { new Guid("f87d6ebd-046a-48f1-aac9-54e5b97435c1"), "Fruits", "Product 4", 687, 144 },
                    { new Guid("fa3eb05d-9ab5-4370-a38d-37fd6ccc091a"), "Beverages", "Product 2", 242, 129 },
                    { new Guid("fb36e2dc-f5e6-404f-bdfb-3a5546041d08"), "Beverages", "Product 20", 572, 112 },
                    { new Guid("fd2e51b3-bca0-4ceb-ada5-a4545eeb33f4"), "Fruits", "Product 13", 495, 35 },
                    { new Guid("ffa32222-42d2-488e-b94b-086f993b3a5a"), "Beverages", "Product 10", 554, 140 },
                    { new Guid("ffcc72ad-56fa-404e-a8fd-c1c9f2e3ad94"), "Beverages", "Product 99", 954, 191 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
