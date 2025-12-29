#!/usr/bin/env bash
set -e

echo "🔍 Running CI validation locally..."
echo ""

echo "📦 Restoring dependencies..."
dotnet restore --nologo

echo ""
echo "🏗️  Building (Release)..."
dotnet build --configuration Release --no-restore --nologo

echo ""
echo "🧪 Running tests..."
dotnet test --configuration Release --no-build --nologo --verbosity normal

echo ""
echo "✅ All validation passed! Safe to push."
