#!/usr/bin/env bash
# Pre-commit validation check
# Run this before committing to catch issues early

set -e

echo "🔍 Pre-commit validation starting..."
echo ""

# Check for common issues before building
echo "📋 Checking for common issues..."

# Check for bin/obj in staged files
if git diff --cached --name-only | grep -qE '^(bin|obj)/'; then
    echo "❌ ERROR: bin/ or obj/ directories in staged changes"
    echo "   Run: git reset HEAD bin/ obj/"
    exit 1
fi

# Check for .vs or IDE files
if git diff --cached --name-only | grep -qE '\.(vs|idea|vscode)/'; then
    echo "⚠️  WARNING: IDE files in staged changes"
fi

# Check for missing files referenced in changed .csproj files
for file in $(git diff --cached --name-only | grep '\.csproj$'); do
    if grep -q 'aifirst-icon.png' "$file" 2>/dev/null; then
        if [ ! -f "assets/aifirst-icon.png" ]; then
            echo "❌ ERROR: $file references aifirst-icon.png but file doesn't exist"
            exit 1
        fi
    fi
done

echo "✅ Pre-checks passed"
echo ""

# Run full validation
./scripts/validate-ci.sh

echo ""
echo "✅ All pre-commit validation passed! Safe to commit."
