import requests

# Constants
DRAFT_ID = 4  # Replace with your actual draft ID
BASE_URL = "http://localhost:5062/api/draft"

def run_draft(draft_id):
    try:
        response = requests.post(f"{BASE_URL}/{draft_id}/run-draft")
        response.raise_for_status()
        print("Draft completed successfully!")
    except requests.exceptions.RequestException as e:
        print(f"Error running draft: {e}")

if __name__ == "__main__":
    run_draft(DRAFT_ID)
