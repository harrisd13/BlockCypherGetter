db.createUser(
    {
        user: "admin",
        pwd: "Test123!",
        roles: [
            {
                role: "readWrite",
                db: "BlockCypher"
            }
        ]
    }
);

db.createCollection(
"blockchain",
{
  timeseries: {
  timeField: "CreatedAt",
  metaField: "Metadata"
}});

db.blockchain.createIndex( { "Metadata.name": 1,  "CreatedAt": -1 });
