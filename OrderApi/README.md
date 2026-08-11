dotnet-kafka-event-driven-demo/
│
├── OrderApi/
│   ├── Controllers/
│   ├── Services/
│   ├── Program.cs
│   ├── Dockerfile
│   └── OrderApi.csproj
│
├── OrderConsumer/
│   ├── KafkaConsumerService.cs
│   ├── Program.cs
│   └── OrderConsumer.csproj
│
├── Kubernetes/
│   ├── orderapi-deployment.yaml
│   └── orderapi-service.yaml
│
└── README.md

OrderApi
   ↓
Kafka Producer
   ↓
Kafka
   ↓
order-created topic
   ↓
OrderConsumer BackgroundService

git init
git add .
git commit -m "Initial Kafka event driven .NET demo"
git branch -M main
git remote add origin <your-github-repository-url>
git push -u origin main



echo "# dotnet-kafka-event-driven-demo" >> README.md
git init
git add README.md
git commit -m "first commit"
git branch -M main
git remote add origin https://github.com/arulkg17/dotnet-kafka-event-driven-demo.git
git push -u origin main





